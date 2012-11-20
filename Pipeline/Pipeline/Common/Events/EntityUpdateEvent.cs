
using Pipeline.Model;
namespace Pipeline.Events
{
    public abstract class EntityUpdateEvent : EntityEvent
    {
        public EntityUpdateEvent(EntityIdentifier entityIdentifier) : base(entityIdentifier)
        {
        }
    }
}