using Pipeline.Events;
using Pipeline.Model;
using Pipeline.XmlTools;

namespace BasicExample.Entity.Person
{
    public class PersonRegisteredEvent : EntityPostEvent
    {
        public PersonRegisteredEvent(Xml xmlContent, EntityIdentifier entityIdentifier)
            : base(xmlContent, entityIdentifier)
        {

        }
    }
}
