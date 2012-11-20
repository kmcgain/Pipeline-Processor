using System;

namespace Pipeline.Pipe
{
    using global::Pipeline.Events;
    using Results;

    public interface IPipelineProcessor
    {
        ///<summary>
        /// Propagate the event over the transport.
        ///</summary>
        ///<param name="payload"> </param>
        ///<param name="event"></param>
        ///<typeparam name="TEvent"></typeparam>
        PipelineResult Process<TEvent>(Payload<TEvent> payload) where TEvent : IEvent;

    }
}