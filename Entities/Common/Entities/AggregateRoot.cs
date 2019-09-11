namespace Entities.Common.Entities
{
    using Contracts.Events;
    using System.Collections.Generic;

    public abstract class AggregateRoot : Entity
    {
        private readonly List<IntegrationEvent> _integrationEvents = new List<IntegrationEvent>();

        public IReadOnlyList<IntegrationEvent> IntegrationEvents => _integrationEvents;

        protected AggregateRoot()
        {
        }

        public virtual void AddIntegrationEvent(IntegrationEvent integrationEvent)
        {
            _integrationEvents.Add(integrationEvent);
        }
    }
}
