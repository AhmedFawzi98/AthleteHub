using Microsoft.OpenApi.Models;
using Resturants.Infrastructure.CustomJsonConverters;
using Serilog;
using AthleteHub.Infrastructure.Constants;
using AthleteHub.Api.Middlewares;
using Microsoft.OpenApi.Any;

namespace AthleteHub.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
        #region Logger

        var loggerConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(loggerConfig)
            .WriteTo.File(
                path: "wwwroot/SeriLogs/logs-.log",
                outputTemplate: "[{Level}] {Timestamp:dd-MM HH:mm:ss} || {Message}{NewLine}{Exception}",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true
            )
            .CreateLogger();

        services.AddSerilog();
        #endregion

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
            options.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date",
                Example = new OpenApiString(DateTime.Today.ToString("yyyy-MM-dd"))
            });
        });
        #endregion

        services.AddExceptionHandler<UnAuthorizedExceptionHandler>();
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }
}
