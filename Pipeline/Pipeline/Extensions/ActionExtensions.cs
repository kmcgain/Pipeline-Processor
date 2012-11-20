using System;

namespace Pipeline.Model.Extensions
{
    using Pipe;
    using global::Pipeline.Events;
    using Results;

    public static class ActionExtensions
    {
        public static PipelineResult ProcessAction<TEvent>(this IPipelineProcessor processor, TEvent @event)
            where TEvent : IEvent
        {
            return processor.Process(new Payload<TEvent>(@event));
        }

        public static PipelineResult ProcessActionForEvent(this IPipelineProcessor processor, IEvent @event)
        {
            var payloadType = typeof (Payload<>).MakeGenericType(@event.GetType());

            var payloadInstance = Activator.CreateInstance(payloadType, @event);

            var processMethod = typeof(IPipelineProcessor).GetMethod("Process");
            var genProcessMethod = processMethod.MakeGenericMethod(@event.GetType());

            return (PipelineResult)genProcessMethod.Invoke(processor, new []{payloadInstance});
        }
    }
}