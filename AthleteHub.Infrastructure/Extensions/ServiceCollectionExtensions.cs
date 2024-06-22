using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Interfaces.Repositories;
using AthleteHub.Infrastructure.Constants;
using AthleteHub.Infrastructure.Persistance;
using AthleteHub.Infrastructure.Repositories;
using AthleteHub.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
        })
       .AddEntityFrameworkStores<AthleteHubDbContext>()
       .AddDefaultTokenProviders()
       .AddRoles<IdentityRole>();

        services.AddScoped<ISeeder, Seeder>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
