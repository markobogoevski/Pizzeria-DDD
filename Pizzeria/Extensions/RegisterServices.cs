namespace Pizzeria.Extensions
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Pizzeria.Contracts;
    using Pizzeria.Services;

    /// <summary>
    /// Class used for registering the needed services for the pizzeria module.
    /// </summary>
    public static class RegisterServices
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAppSettings>(provider => new AppSettings(configuration));

        }

        public static void AddServices(this IServiceCollection services, IHostingEnvironment environment)
        {
            IAppSettings appSettings = services.BuildServiceProvider().GetService<IAppSettings>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddPizzeriaServices(appSettings);
        }

    }
}
