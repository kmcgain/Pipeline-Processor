using Pipeline.Pipe;
using Pipeline.Results;

namespace BasicExample.Entity.Person
{
    public class ValidatePersonPost : IProcess<PersonRegisteredEvent>, Order.EntityPrecondition
    {
        public Outcome Receive(IPayload<PersonRegisteredEvent> payload)
        {
            if (payload.Event.Xml.XPathSelect("person") == null)
            {
                return new Outcome(403, "Not a valid person");
            }

            return Pipeline.Results.Pipeline.Continue;
        }
    }
}