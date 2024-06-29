using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Enums;
using AthleteHub.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AthleteHub.Infrastructure.Seeders;

internal class Seeder(AthleteHubDbContext _context, UserManager<ApplicationUser> _userManager) : ISeeder
{
    private ApplicationUser admin = new ApplicationUser()
    {
        UserName = "admin1",
        NormalizedUserName = "admin1".ToUpper(),
        Email = "admin1@athletehub.com",
        NormalizedEmail = "admin1@athletehub.com".ToUpper(),
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        FirstName = "admin",
        LastName = "admin",
        Gender = Gender.Male,
        PhoneNumber = "2434544646",
        DateOfBirth = new DateOnly(2000, 2, 2)
    };
    private ApplicationUser coach = new ApplicationUser()
    {
        UserName = "coach1",
        NormalizedUserName = "coach1".ToUpper(),
        Email = "coach1@athletehub.com",
        NormalizedEmail = "coach1@athletehub.com".ToUpper(),
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        FirstName = "coach",
        LastName = "coach",
        Gender = Gender.Male,
        PhoneNumber = "2434544646",
        DateOfBirth = new DateOnly(2000, 2, 2)
    };
    private ApplicationUser athlete = new ApplicationUser()
    {
        UserName = "athlete1",
        NormalizedUserName = "athlete1".ToUpper(),
        Email = "athlete1@athletehub.com",
        NormalizedEmail = "athlete1@athletehub.com".ToUpper(),
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        FirstName = "athlete",
        LastName = "athlete",
        Gender = Gender.Male,
        PhoneNumber = "2434544646",
        DateOfBirth = new DateOnly(2000, 2, 2)
    };
    public async Task SeedAsync()
    {
        if(_context.Database.GetPendingMigrations().Any())
        {
            await _context.Database.MigrateAsync();
        }

        if (await _context.Database.CanConnectAsync())
        {
            if (!_context.Roles.Any())
            {
                var roles = GetRoles();
                await _context.Roles.AddRangeAsync(roles);
                await _context.SaveChangesAsync();
            }
            if (!_context.Users.Any())
            {
                var resultAdmin = await _userManager.CreateAsync(admin, "adminAdmin@12345@");
                var resultCoach = await _userManager.CreateAsync(coach, "coachCoach@12345@");
                var resultAthlete = await _userManager.CreateAsync(athlete, "athleteAthlete@12345@");

                if (resultAdmin.Succeeded && resultCoach.Succeeded && resultAthlete.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, RolesConstants.Admin);
                    await _userManager.AddToRoleAsync(coach, RolesConstants.Coach);
                    await _userManager.AddToRoleAsync(athlete, RolesConstants.Athlete);
                }
            }
            if(!_context.Features.Any())
            {
                var features = GetFeatures();
                await _context.Features.AddRangeAsync(features);
                await _context.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Feature> GetFeatures()
    {
        IEnumerable<Feature> features = new List<Feature>()
        {
            new Feature(){Name="Custom Nutrition Plans", Description="Create personalized nutrition plans based on client preferences."},
            new Feature(){Name="Workout Tracking", Description="Track client workouts and progress over time."},
            new Feature(){Name="Real-time Chat Messaging (Daily)", Description="Communicate with clients via real-time chat every day."},
            new Feature(){Name="Real-time Chat Messaging (Weekly)", Description="Communicate with clients via real-time chat every week."},
            new Feature(){Name="Custom Training Programs", Description="Design customized training programs for individual clients."},
            new Feature(){Name="Performance Reviews", Description="Conduct regular performance reviews to assess client progress and adjust strategies."},
            new Feature(){Name="Goal Setting", Description="Set and monitor client goals."},
            new Feature(){Name="Exercise Video Tutorials", Description="Access a library of exercise video tutorials for clients to follow along."},
            new Feature(){Name="Progress Assessments", Description="Perform weekly assessments and track client progress."},
            new Feature(){Name="Calorie Tracking", Description="Track daily calorie intake and expenditure."},
            new Feature(){Name="Personal Live Workout Sessions", Description="Schedule personalized one-on-one workout sessions with live coaching."},
            new Feature(){Name="Competition Preparation", Description="Prepare clients for competitions with specialized training plans and strategies."}
        };

        return features;
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        IEnumerable<IdentityRole> roles = new List<IdentityRole>
        {
            CreateRole(RolesConstants.Admin),
            CreateRole(RolesConstants.Coach),
            CreateRole(RolesConstants.Athlete)
        };

        return roles;
    }
    private IdentityRole CreateRole(string roleName)
    {
        var role = new IdentityRole(roleName);
        role.NormalizedName = roleName.ToUpper();
        role.ConcurrencyStamp = Guid.NewGuid().ToString();

        return role;
    }
}
