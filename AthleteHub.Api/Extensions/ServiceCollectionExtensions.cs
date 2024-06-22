using Microsoft.OpenApi.Models;
using Resturants.Infrastructure.CustomJsonConverters;

namespace AthleteHub.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter())
        );

        services.AddCors(options =>
            options.AddPolicy(CorsPoliciesConstants.AllowAll, policy =>
            {
                policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            })
        );

        services.AddEndpointsApiExplorer();


        #region Swagger
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "Version 1",
                Title = "AthleteHub API",
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Description = "enter token:"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new string[] { }
                }
            });
        });
        #endregion
    }
}
