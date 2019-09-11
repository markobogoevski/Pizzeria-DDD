namespace Storage.Common.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Pizzeria.Storage.Common.Entities;
    using Storage.Common.Database.Context;
    using System;
    using System.Linq;

    public class Repository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        protected readonly IPizzeriaDbContext _pizzaDbContext;

        public Repository(IPizzeriaDbContext hoursDbContext)
        {
            _pizzaDbContext = hoursDbContext;
        }

        protected IQueryable<Entity> AllOf<Entity>() where Entity : BaseEntity
        {
            return _pizzaDbContext.Set<Entity>().Where(x => x.DeletedOn == null);
        }

        protected IQueryable<Entity> AllNoTrackedOf<Entity>() where Entity : BaseEntity
        {
            return AllOf<Entity>().AsNoTracking();
        }

        protected void AttachOrUpdate<Entity>(Entity entity, EntityState entityState) where Entity : AggregateRoot
        {
            Entity existingEntity = _pizzaDbContext.Set<Entity>().Local.SingleOrDefault(e => e.Id == entity.Id);

            if (existingEntity == null)
            {
                _pizzaDbContext.Entry(entity).State = entityState;
            }
            else
            {
                _pizzaDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
        }

        /// <summary>
        /// An insert method that inserts the entity, begins tracking it and assigns Id to it
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected void Insert(TAggregateRoot entity)
        {
            entity.CreatedOn = DateTime.UtcNow;
            _pizzaDbContext.Set<TAggregateRoot>().Add(entity);
        }
    }
}
