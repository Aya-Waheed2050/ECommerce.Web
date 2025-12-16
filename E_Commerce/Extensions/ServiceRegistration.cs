using System.Text;
using E_Commerce.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Common;

namespace E_Commerce.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(options => 
            {
                options.CustomSchemaIds(type => type.FullName);
                options.AddSecurityDefinition("Bearer" , new OpenApiSecurityScheme() 
                {
                     In = ParameterLocation.Header,
                     Name = "Authorization",
                     Type = SecuritySchemeType.ApiKey,
                     Scheme = "Bearer",
                     Description = "Enter 'Bearer' Followed By Space And Your Token",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {  
                    {
                         new OpenApiSecurityScheme()
                         {
                             Reference = new OpenApiReference()
                             {
                                 Id = "Bearer",
                                 Type = ReferenceType.SecurityScheme
                             }
                         },
                         new string[]{ }
                    }
                });
            });

            return Services;
        }

        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services , IConfiguration _configuration)
        {
            // Change Default api Error Response
            Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
            });

            //Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll", build =>
            //    {
            //        build.AllowAnyHeader().AllowAnyMethod()
            //        .WithOrigins(_configuration.GetSection("Urls")["FrontUrl"]);
            //    });
            //});

            return Services;
        }

        public static IServiceCollection AddJwtServices(this IServiceCollection Services , IConfiguration _configuration)
        {
            var jwtOptions = _configuration.GetSection("JwtOptions").Get<JwtOptions>();

            // Change Default api Error Response
            Services.AddAuthentication((Config) =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,

                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),

                };
            });

            return Services;
        }

    }
}
