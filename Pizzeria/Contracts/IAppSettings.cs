namespace Pizzeria.Contracts
{
    using Pizzeria.Services;

    /// <summary>
    /// Interface for app settings.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Gets api documentation settings.
        /// </summary>
        ApiDocsSettings ApiDocsSettings { get; }

        /// <summary>
        /// Connection strings from settings.
        /// </summary>
        ConnectionStrings ConnectionStrings { get; }
    }
}
