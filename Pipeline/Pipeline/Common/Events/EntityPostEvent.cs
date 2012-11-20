namespace Pipeline.Events
{
    using Model;
    using Pipeline.XmlTools;

    public abstract class EntityPostEvent : EntityUpdateEvent
    {
        protected EntityPostEvent(Xml xmlContent, EntityIdentifier entityIdentifier) 
            : base(entityIdentifier)
        {
            Xml = xmlContent;
        }

        public Xml Xml { get; private set; }
    }
}