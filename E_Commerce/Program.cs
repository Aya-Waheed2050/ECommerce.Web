using E_Commerce.Extensions;
using Persistence;
using Service;
namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
            builder.Services.AddSwaggerServices();
           

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationService(builder.Configuration);
            builder.Services.AddWebApplicationServices(builder.Configuration);

            builder.Services.AddJwtServices(builder.Configuration);
            #endregion

            var app = builder.Build();

            await app.SeedDataBaseAsync();


            #region Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddleWare();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();
            }
            
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            #endregion


            app.Run();
        }
    }
}
