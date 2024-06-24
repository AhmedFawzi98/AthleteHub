using AthleteHub.Application.Services.FilterService;
using AthleteHub.Application.Services.SearchService;
using AthleteHub.Application.Services.SortingService;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace AthleteHub.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddAutoMapper(applicationAssembly);

        services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

        services.AddHttpContextAccessor();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly));

        services.AddScoped<IFilterService, FilterService>();

        services.AddScoped<ISearchService, SearchService>();

        services.AddScoped<ISortService, SortService>();
    }
}
