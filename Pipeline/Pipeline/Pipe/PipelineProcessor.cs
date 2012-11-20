

namespace Pipeline.Pipe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Pipeline.Events;
    using Results;
    using StructureMap;
    using global::Pipeline.Model.Extensions;

    public interface IEventLogger { void Log(Exception e); void Log<T>(IPayload<T> p) where T : IEvent; }

    public class PipelineProcessor : IPipelineProcessor
    {
        private readonly IContainer container;
        private readonly IEventLogger eventLogger;

        public PipelineProcessor(IContainer container, IEventLogger logger)
        {
            this.container = container;
            this.eventLogger = logger;
        }

        ///<summary>
        /// Propagate the event over the transport.
        ///</summary>
        ///<param name="payload"> </param>
        ///<typeparam name="TEvent"></typeparam>
        public PipelineResult Process<TEvent>(Payload<TEvent> payload) where TEvent : IEvent
        {
            var allProcessors = DetermineProcessors<TEvent>();

            foreach (var pipelineStep in Order.RunOrder)
            {
                var capturedStep = pipelineStep;
                foreach (var processor in allProcessors.Where(processor => capturedStep.IsInstanceOfType(processor)))
                {
                    var stepCallInformation = new StepCallInformation(processor.GetType());
                    payload.StepsCalled.Add(stepCallInformation);

                    stepCallInformation.StartStep();

                    try
                    {
                        Outcome result = null;
                        try
                        {
                            result = callReceiver(payload, processor);
                        }
                        finally
                        {
                            stepCallInformation.StopStep(result);
                        }

                        if (result == Pipeline.Continue) continue;

                        //Log failures
                        if (!(result.PipelineResult is SuccessResult) && !(result.PipelineResult is HtmlResult))
                            eventLogger.Log(payload);

                        //We stop the pipeline and return the result from the processor
                        return result.PipelineResult;
                    }
                    catch (Exception e)
                    {
                        eventLogger.Log(e);
                        throw;
                    }
                }
            }

            throw new InvalidOperationException("Out of pipeline steps with no outcome");
        }

        private static Outcome callReceiver<TEvent>(Payload<TEvent> payload, IProcess<TEvent> processor) where TEvent : IEvent
        {
            var mostSpecificReceiver = getMostSpecificReceiver(processor);
            return (Outcome) mostSpecificReceiver.Invoke(processor, new object[] {payload});
        }

        private static MethodInfo getMostSpecificReceiver<TEvent>(IProcess<TEvent> processor) where TEvent : IEvent
        {
            var allPublicMethods = processor.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var allReceivers = allPublicMethods.Where(_ => _.Name == "Receive").Materialise();

            foreach (var eventType in getEventTypeHierarchy(typeof (TEvent)))
            {
                var matchingReceiver =
                    allReceivers
                        .FirstOrDefault(
                            methodInfo =>
                            methodInfo.GetParameters().Single().ParameterType == typeof (IPayload<>).MakeGenericType(eventType));

                if (matchingReceiver != null)
                    return matchingReceiver;
            }

            throw new InvalidOperationException("No receiver found");
        }

        public IList<IProcess<TEvent>> DetermineProcessors<TEvent>() where TEvent : IEvent
        {
            var allProcessors = getAllProcessorsForTypes<TEvent>(getEventTypeHierarchy(typeof (TEvent)));

            return allProcessors.Materialise();
        }

        private static IEnumerable<Type> getEventTypeHierarchy(Type t)
        {
            //Each type in the event hierarchy from leaf to root parent (below object).
            //Concatenated with all interfaces derived from IEvent, ignoring types already in the list.
            foreach (var type in GetEventTypeHierarchyFor(t))
            {
                yield return type;
            }

            foreach (var type in GetEventInterfacesFor(t))
            {
                yield return type;
            }
        }


        public static IEnumerable<Type> GetEventTypeHierarchyFor(Type t)
        {
            do
            {
                yield return t;
                t = t.BaseType;
            } while (t != null && t != typeof (object));
        }

        private static IEnumerable<Type> GetEventInterfacesFor(Type t)
        {
            return t.GetInterfaces().Where(iface => typeof (IEvent).IsAssignableFrom(iface));
        }

        private IEnumerable<IProcess<TEvent>> getAllProcessorsForTypes<TEvent>(IEnumerable<Type> eventTypes) where TEvent : IEvent
        {
            IEnumerable<IProcess<TEvent>> localProcessors = new List<IProcess<TEvent>>();

            foreach (var eventType in eventTypes)
            {
                var processorsForThisType =
                    container.GetAllInstances(typeof (IProcess<>).MakeGenericType(eventType))
                        .Cast<IProcess<TEvent>>();
                localProcessors = localProcessors.Union(processorsForThisType, new ConcreteProcessorComparer<TEvent>());
            }

            return localProcessors;
        }


        public class ConcreteProcessorComparer<TEvent> : IEqualityComparer<IProcess<TEvent>> where TEvent : IEvent
        {
            /// <summary>
            /// Determines whether the specified objects are equal.
            /// </summary>
            /// <returns>
            /// true if the specified objects are equal; otherwise, false.
            /// </returns>
            /// <param name="x">The first object of type <paramref name="T"/> to compare.</param><param name="y">The second object of type <paramref name="T"/> to compare.</param>
            public bool Equals(IProcess<TEvent> x, IProcess<TEvent> y)
            {
                return x.GetType() == y.GetType();
            }

            /// <summary>
            /// Returns a hash code for the specified object.
            /// </summary>
            /// <returns>
            /// A hash code for the specified object.
            /// </returns>
            /// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
            public int GetHashCode(IProcess<TEvent> obj)
            {
                //Force the comparison by making everything's hashcode the same.
                return 0;
            }
        }

        public class PipelineOrderComparer : IComparer<Type>
        {
            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <returns>
            /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>, as shown in the following table.Value Meaning Less than zero<paramref name="x"/> is less than <paramref name="y"/>.Zero<paramref name="x"/> equals <paramref name="y"/>.Greater than zero<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            /// <param name="x">The first object to compare.</param><param name="y">The second object to compare.</param>
            public int Compare(Type x, Type y)
            {
                var pipelineInterfaceForX = x.GetInterfaces().First(processor => typeof (IPipelineStep).IsAssignableFrom(processor));
                var pipelineInterfaceForY = y.GetInterfaces().First(processor => typeof (IPipelineStep).IsAssignableFrom(processor));

                return Order.RunOrder.IndexOf(pipelineInterfaceForX).CompareTo(Order.RunOrder.IndexOf(pipelineInterfaceForY));
            }
        }
    }
}