
﻿using AthleteHub.Application.Users;
using AthleteHub.Application.Services;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Interfaces.Repositories;
using AthleteHub.Infrastructure.Authorization.Services;
using AthleteHub.Infrastructure.Authorization.Services.Payment;
using AthleteHub.Infrastructure.Authorization.Services;


using AthleteHub.Application.Services.BlobStorageService;

using AthleteHub.Infrastructure.BlobStorage;

using AthleteHub.Infrastructure.Configurations;
using AthleteHub.Infrastructure.Constants;
using AthleteHub.Infrastructure.Persistance;
using AthleteHub.Infrastructure.Repositories;
using AthleteHub.Infrastructure.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AthleteHub.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string AthleteHubDbConnectionString = configuration.GetConnectionString(AppSettingsConstants.AthleteHub)!;

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

            options.SignIn.RequireConfirmedEmail = false;

            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
            options.Lockout.MaxFailedAccessAttempts = 5;
        })
       .AddEntityFrameworkStores<AthleteHubDbContext>()
       .AddDefaultTokenProviders()
       .AddRoles<IdentityRole>();

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
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddMemoryCache();


        services.AddScoped<ITokenService, TokenService>();
        services.Configure<BlobStorageSettings>(configuration.GetSection(BlobStorageSettings.BlobStorage));
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
}
