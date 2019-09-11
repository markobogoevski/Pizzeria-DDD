using System;
namespace Pizzeria.Contracts
{
    public class ApiVersionSettings
    {
        public string Name { get; set; }

        public string JsonEndpointUrl { get; set; }

        public static ApiVersionSettings Create(string name, string jsonEndpointUrl)
        {
            return new ApiVersionSettings
            {
                Name = name,
                JsonEndpointUrl = jsonEndpointUrl
            };
        }
    }
}
