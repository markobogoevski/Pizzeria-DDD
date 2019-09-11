namespace Pizzeria.Storage.Common.Entities
{
    using MediatR;
    using System.Collections.Generic;

    public abstract class TrackedEntity : Entity
    {
        protected TrackedEntity(IEnumerable<INotification> domainEvents) : base(domainEvents)
        {
        }

        protected TrackedEntity()
        {
        }

        public string PerformedByEmail { get; internal set; }

        public string PerformedByAppName { get; internal set; }
    }
}
