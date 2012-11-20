using System;

namespace Pipeline.Pipe
{
    using System.Collections.Generic;
    using Pipeline.Events;

    public class Payload<TEvent> : IPayload<TEvent> where TEvent : IEvent
    {
        public Payload(TEvent @event)
        {
            Event = @event;
            StepsCalled = new List<StepCallInformation>();
        }

        public TEvent Event { get; private set; }

        public IList<StepCallInformation> StepsCalled { get; set; }

    }
}