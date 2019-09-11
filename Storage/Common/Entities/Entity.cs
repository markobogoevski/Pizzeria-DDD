namespace Pizzeria.Storage.Common.Entities
{
    using MediatR;
    using System;
    using System.Collections.Generic;

    public abstract class Entity : BaseEntity
    {
        protected Entity(IEnumerable<INotification> domainEvents) : base(domainEvents)
        {
        }

        protected Entity()
        {
        }

        public Guid Uid { get; protected internal set; }
    }
}
