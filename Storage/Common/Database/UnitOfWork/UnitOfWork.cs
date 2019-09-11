namespace Storage.Common.Database.UnitOfWork
{
    using Contracts.Common.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Pizzeria.Storage.Common.Entities;
    using Storage.Common.Database.Context;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IPizzeriaDbContext _pizzaDbContext;

        private readonly EntityState[] _trackedEntityStates = new[]
        {
            EntityState.Deleted,
            EntityState.Modified,
            EntityState.Added
        };

        public UnitOfWork(IPizzeriaDbContext pizzaDbContext)
        {
            _pizzaDbContext = pizzaDbContext;
        }

        public void Dispose()
        {
            _pizzaDbContext.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            if (_pizzaDbContext.ChangeTracker.HasChanges())
            {
                IEnumerable<EntityEntry<TrackedEntity>> changedEntities = _pizzaDbContext.ChangeTracker
                                                                                         .Entries<TrackedEntity>()
                                                                                         .Where(e => _trackedEntityStates.Contains(e.State));
            }

            int result = await _pizzaDbContext.SaveChangesAsync();

            return result;
        }
    }

}
