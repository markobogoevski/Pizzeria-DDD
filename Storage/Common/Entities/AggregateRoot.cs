namespace Pizzeria.Storage.Common.Entities
{
    using Contracts.Events;
    using MediatR;
    using System;
    using System.Collections.Generic;

    public abstract class AggregateRoot : TrackedEntity
    {
        private readonly List<IntegrationEvent> _integrationEvents = new List<IntegrationEvent>();

        public IReadOnlyList<IntegrationEvent> IntegrationEvents => _integrationEvents;

        public void ClearIntegrationEvents()
        {
            _integrationEvents.Clear();
        }

        protected AggregateRoot() : this(new List<INotification>(), new List<IntegrationEvent>())
        {

        }

        protected AggregateRoot(IEnumerable<INotification> domainEvents, IEnumerable<IntegrationEvent> integrationEvents) : base(domainEvents)
        {
            if (integrationEvents == null)
            {
                throw new ArgumentNullException(nameof(domainEvents), "Integration events can not be null.");
            }

            _integrationEvents.AddRange(integrationEvents);
        }
    }
}
