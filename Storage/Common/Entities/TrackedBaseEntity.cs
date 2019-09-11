namespace Pizzeria.Storage.Common.Entities
{
    using MediatR;
    using System.Collections.Generic;

    public abstract class TrackedBaseEntity : BaseEntity
    {
        protected TrackedBaseEntity(IEnumerable<INotification> domainEvents) : base(domainEvents)
        {
        }

        protected TrackedBaseEntity()
        {
        }

        public string PerformedByEmail { get; internal set; }

        public string PerformedByAppName { get; internal set; }
    }
}
