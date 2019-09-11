namespace Pizzeria.Services
{
    using Microsoft.Extensions.Configuration;
    using Pizzeria.Contracts;

    internal class AppSettings : SettingsBase, IAppSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        /// <param name="configuration"> Application configuration.</param>
        internal AppSettings(IConfiguration configuration) : base(configuration)
        {
        }

        private ApiDocsSettings _apiDocsSettings;
        private ConnectionStrings _connectionStrings;

        /// <summary>
        /// Gets the api document settings.
        /// </summary>
        public ApiDocsSettings ApiDocsSettings => _apiDocsSettings ?? (_apiDocsSettings = GetSection<ApiDocsSettings>("apiDocs"));

        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        public ConnectionStrings ConnectionStrings => _connectionStrings ?? (_connectionStrings = GetSection<ConnectionStrings>("connectionStrings"));
    }
}
