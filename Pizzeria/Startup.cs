namespace Pizzeria
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using Pizzeria.Contracts;
    using Swashbuckle.AspNetCore.Swagger;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using Pizzeria.Extensions;
    using Pizzeria.Statics;
    using System.Linq;

    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration,
                       IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSettings(_configuration);
            services.AddServices(_environment);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new MediaTypeApiVersionReader();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersions.ApiVersion1_0, new Info { Title = ApiVersions.ApiVersion1_0, Version = ApiVersions.ApiVersion1_0 });
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var actionApiVersionModel = apiDesc.ActionDescriptor?.ToApiVersion();

                    if (actionApiVersionModel == null)
                    {
                        // would mean this action is not version-ed and should be included everywhere
                        return true;
                    }

                    return actionApiVersionModel.ImplementedApiVersions.Any(v => v.ToString() == docName);
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IAppSettings appSettings = app.ApplicationServices.GetService<IAppSettings>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = appSettings.ApiDocsSettings.RouteTemplate;
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = appSettings.ApiDocsSettings.RoutePrefix;
                c.DocExpansion(DocExpansion.None);
                c.SwaggerEndpoint(appSettings.ApiDocsSettings.Version1_0.JsonEndpointUrl, appSettings.ApiDocsSettings.Version1_0.Name);
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
