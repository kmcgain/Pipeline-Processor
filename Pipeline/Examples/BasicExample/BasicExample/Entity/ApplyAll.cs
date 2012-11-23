using Pipeline.Events;
using Pipeline.Pipe;
using Pipeline.Results;
using Pipeline.XmlTools;

namespace BasicExample.Entity
{
    public class ApplyAll : IProcess<EntityPostEvent>, Order.EntityPrecondition
    {
        public Outcome Receive(IPayload<EntityPostEvent> payload)
        {
            return new Outcome(new SuccessResult(new Xml("<result>Event applied</result>")));
        }
    }
}