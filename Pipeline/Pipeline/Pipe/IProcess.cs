using Pipeline.Events;
using Pipeline.Results;
namespace Pipeline.Pipe
{

    public interface IProcess<in TEvent> where TEvent : IEvent
    {
        Outcome Receive(IPayload<TEvent> payload);
    }
}