namespace Entities.Common.Entities
{
    using global::Entities.Common.Enumerations;
    using MediatR;
    using System;
    using System.Collections.Generic;

    public abstract class BaseEntity
    {
        private readonly List<INotification> _domainEvents = new List<INotification>();

        public int Id { get; protected set; }

        public DateTime CreatedOn { get; protected set; }

        public DateTime? DeletedOn { get; protected set; }

        public EntityState State { get; protected set; }

        public bool IsChanged => State != EntityState.Unchanged;

        public IReadOnlyList<INotification> DomainEvents => _domainEvents;

        protected BaseEntity()
        {
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is BaseEntity)
            {
                return Id.Equals((obj as BaseEntity).Id);
            }

            return false;
        }

        protected void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        protected void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents.Remove(eventItem);
        }

        public void MarkAsDeleted(DateTime deletedOn)
        {
            DeletedOn = deletedOn;

            MarkModify();
        }

        public void MarkModify()
        {
            State = EntityState.Modified;
        }

        public void MarkAdded()
        {
            State = EntityState.Added;
        }

        public static bool operator true(BaseEntity baseEntity)
        {
            return baseEntity == null;
        }

        public static bool operator false(BaseEntity baseEntity)
        {
            return baseEntity != null;
        }
    }
}
