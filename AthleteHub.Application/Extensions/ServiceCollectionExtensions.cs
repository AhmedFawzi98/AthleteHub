using AthleteHub.Application.Users;
﻿using AthleteHub.Application.Services.FilterService;
using AthleteHub.Application.Services.SearchService;
using AthleteHub.Application.Services.SortingService;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using AthleteHub.Application.Services;
using AthleteHub.Domain.Interfaces.Services;

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

        services.AddScoped<IFilterService,FilterService>();
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<ISortingService, SortingService>();
 
    }
}
