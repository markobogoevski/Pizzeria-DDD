namespace Storage.Common.Database.Context
{
    using Microsoft.EntityFrameworkCore;
 
    public class PizzeriaDbContext : DbContext, IPizzeriaDbContext
    {
        public PizzeriaDbContext(DbContextOptions<PizzeriaDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
