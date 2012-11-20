

namespace Pipeline.Pipe
{
    using System.Collections.Generic;
    using Pipeline.Events;


    public interface IPayload<out TEvent> where TEvent : IEvent
    {
        TEvent Event { get; }
        IList<StepCallInformation> StepsCalled { get; set; }
    }
}