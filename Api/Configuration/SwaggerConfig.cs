using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwggerConfig(this IServiceCollection services)
        {
            

            return services;
        }
        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {

            

            return app;
        }
    }
}
