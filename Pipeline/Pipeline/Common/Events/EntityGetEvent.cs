namespace Pipeline.Events
{
    using Model;

    public abstract class EntityGetEvent : EntityEvent
    {
        protected EntityGetEvent(EntityIdentifier entityIdentifier) : base(entityIdentifier)
        {
        }
    }
}