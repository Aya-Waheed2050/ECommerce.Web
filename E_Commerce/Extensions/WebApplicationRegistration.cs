using System.Text.Json;
using Domain.Contracts;
using E_Commerce.CustomMiddleWares;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_Commerce.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task<WebApplication> SeedDataBaseAsync(this WebApplication app)
        {
            using IServiceScope? scoop = app.Services.CreateScope();
            IDataSeeding? Object = scoop.ServiceProvider.GetRequiredService<IDataSeeding>();

            await Object.DataSeedAsync();
            await Object.IdentityDataSeedAsync();

            return app;
        }

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.ConfigObject = new ConfigObject()
                {
                    DisplayRequestDuration = true
                };
                options.DocumentTitle = "E_Commerce Api";
                options.JsonSerializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                options.DocExpansion(DocExpansion.None);
                options.EnableFilter();
                options.EnablePersistAuthorization();
            });

            return app;
        }

    }
}
