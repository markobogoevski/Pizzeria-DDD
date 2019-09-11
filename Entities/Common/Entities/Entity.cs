namespace Entities.Common.Entities
{
    using System;

    public abstract class Entity : BaseEntity
    {
        public Guid Uid { get; protected set; }
    }
}
