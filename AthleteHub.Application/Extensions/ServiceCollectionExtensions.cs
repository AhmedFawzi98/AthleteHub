using AthleteHub.Application.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Resturants.Application.Users;

namespace AthleteHub.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddAutoMapper(applicationAssembly);

        services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly));


        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

    }
}
