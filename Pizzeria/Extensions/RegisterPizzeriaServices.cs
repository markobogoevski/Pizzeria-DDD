namespace Pizzeria.Extensions
{
    using Entities.Pizza.Repositories;
    using global::Contracts.Common.Interfaces;
    using global::Contracts.Pizza.Interfaces;
    using global::Services.Pizza;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Pizzeria.Contracts;
    using global::Storage.Pizza.Repositories;
    using global::Storage.Common.Database.Context;
    using global::Storage.Common.Database.UnitOfWork;

    public static class RegisterHoursServices
    {
        public static void AddPizzeriaServices(this IServiceCollection services, IAppSettings appSettings)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Context
            services.AddScoped<IPizzeriaDbContext, PizzeriaDbContext>();
            services.AddDbContext<PizzeriaDbContext>(options => options.UseSqlServer(appSettings.ConnectionStrings.PizzeriaDb));

            services.AddScoped<IPizzaRepository, PizzaRepository>();
            services.AddScoped<IPizzaService, PizzaService>();
        }
    }
}
