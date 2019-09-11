namespace Pizzeria.Storage.Common.Entities
{
    using MediatR;
    using System;
    using System.Collections.Generic;

    public abstract class BaseEntity
    {
        public int Id { get; protected internal set; }

        public DateTime CreatedOn { get; protected internal set; }

        public DateTime? DeletedOn { get; protected internal set; }

        private readonly List<INotification> _domainEvents = new List<INotification>();

        public IReadOnlyList<INotification> DomainEvents => _domainEvents;

        protected BaseEntity() { }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected BaseEntity(IEnumerable<INotification> domainEvents)
        {
            if (domainEvents == null)
            {
                throw new ArgumentNullException(nameof(domainEvents), "Domain events can not be null.");
            }

            _domainEvents.AddRange(domainEvents);
        }
    }
}
