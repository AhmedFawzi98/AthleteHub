using AthleteHub.Application.Users;
using AthleteHub.Application.Services;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Interfaces.Repositories;
using AthleteHub.Domain.Interfaces.Services;
using AthleteHub.Infrastructure.Authorization.Services;
using AthleteHub.Infrastructure.Authorization.Services.Payment;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Infrastructure.BlobStorage;
using AthleteHub.Infrastructure.Configurations;
using AthleteHub.Infrastructure.Constants;
using AthleteHub.Infrastructure.Persistance;
using AthleteHub.Infrastructure.Repositories;
using AthleteHub.Infrastructure.Seeders;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AthleteHub.Application.Services.EmailService;
using AthleteHub.Infrastructure.EmailService;
using MvcLab6_Trainees.Services.Email;

namespace AthleteHub.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string AthleteHubDbConnectionString = configuration.GetConnectionString(ConfigurationConstants.AthleteHub)!;

        services.AddDbContext<AthleteHubDbContext>(options =>
        {
            options.UseSqlServer(AthleteHubDbConnectionString).EnableSensitiveDataLogging();
        });

        #region Identity and Auth
        var JwtConfigurations = configuration.GetSection(JwtSettings.Jwt);
        services.Configure<JwtSettings>(JwtConfigurations);

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Tokens.EmailConfirmationTokenProvider = ConfigurationConstants.EmailConfirmationTokenProvider;
        })
       .AddEntityFrameworkStores<AthleteHubDbContext>()
       .AddDefaultTokenProviders()
       .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>(ConfigurationConstants.EmailConfirmationTokenProvider)
       .AddRoles<IdentityRole>();

        services.Configure<EmailConfirmationTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromDays(7);
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            var jwtSettings = JwtConfigurations.Get<JwtSettings>();

            options.TokenValidationParameters = new TokenValidationParameters  
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret!)),

                ClockSkew = TimeSpan.Zero
            };    
        });


        #endregion


        services.AddScoped<ISeeder, Seeder>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ISubscribtionService, SubscribtionService>();

        #region hangfire
        services.AddHangfire(config =>
            config.UseSqlServerStorage(configuration.GetConnectionString(ConfigurationConstants.AthleteHub),
            new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(15),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                UseRecommendedIsolationLevel = true,
                QueuePollInterval = TimeSpan.FromHours(8)
            }));

        services.AddHangfireServer();

        var serviceProvider = services.BuildServiceProvider();
        var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();

        recurringJobManager.AddOrUpdate(HangFireTasksConstants.CheckSubscriptionExpirations,
            () => serviceProvider.GetRequiredService<ISubscribtionService>().CheckSubscriptionExpirations(),
            Cron.Daily()
        );

        #endregion

        #region email service
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.EmailService));
        services.AddTransient<IEmailService, EmailService>();
        #endregion
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.Configure<BlobStorageSettings>(configuration.GetSection(BlobStorageSettings.BlobStorage));
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
}
