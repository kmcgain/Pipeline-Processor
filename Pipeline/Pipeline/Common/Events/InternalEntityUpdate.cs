namespace Pipeline.Events
{
    using Model;

    public abstract class InternalEntityUpdate : EntityUpdateEvent
    {
        protected InternalEntityUpdate(EntityIdentifier entityIdentifier) : base(entityIdentifier)
        {
        }
    }
}