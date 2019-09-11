namespace Pizzeria.Extensions
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Versioning;

    public static class ApiExtensions
    {
        public static ApiVersionModel ToApiVersion(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor?.Properties
                                    .Where((kvp) => ((Type)kvp.Key).Equals(typeof(ApiVersionModel)))
                                    .Select(kvp => kvp.Value as ApiVersionModel).FirstOrDefault();
        }
    }
}
