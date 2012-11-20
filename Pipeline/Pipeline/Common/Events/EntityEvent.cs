namespace Pipeline.Events
{
    using Model;

    public abstract class EntityEvent : Event
    {
        protected EntityEvent(EntityIdentifier entityIdentifier)
        {
            EntityIdentifier = entityIdentifier;
        }

        public EntityIdentifier EntityIdentifier { get; private set; }
    }
}