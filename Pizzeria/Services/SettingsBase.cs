namespace Pizzeria.Services
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Settings base that returns value from configuration.
    /// </summary>
    public abstract class SettingsBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsBase"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        protected SettingsBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected T GetValue<T>(string key) => _configuration.GetValue<T>(key);
        protected T GetSection<T>(string key) => _configuration.GetSection(key).Get<T>();
        protected string GetConnectionString(string key) => _configuration.GetConnectionString(key);
    }
}
