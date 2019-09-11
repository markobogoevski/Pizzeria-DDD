namespace Pizzeria.Contracts
{
    public class ApiDocsSettings
    {
        public string RoutePrefix { get; set; }

        public string RouteTemplate { get; set; }

        public ApiVersionSettings Version1_0 { get; set; }
    }
}
