using Microsoft.EntityFrameworkCore;
using MsCatalog.Application.Services;
using MsCatalog.Domain.Ports.Inbound;
using MsCatalog.Domain.Ports.Outbound;
using MsCatalog.Domain.Models.Context;
using MsCatalog.Infrastructure.Adapters;

namespace MsCatalog.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCatalogServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Application Services (Use Cases)
        services.AddScoped<IIdentificationTypeUseCase, IdentificationTypeService>();
        services.AddScoped<IEconomicActivityUseCase, EconomicActivityService>();
        services.AddScoped<IGenderUseCase, GenderService>();
        services.AddScoped<ICountryUseCase, CountryService>();
        services.AddScoped<IProvinceUseCase, ProvinceService>();
        services.AddScoped<ICityUseCase, CityService>();

        // Repositories (Outbound Ports)
        services.AddScoped<IIdentificationTypeAdapter, IdentificationTypeAdapter>();
        services.AddScoped<IEconomicActivityAdapter, EconomicActivityAdapter>();
        services.AddScoped<IGenderAdapter, GenderAdapter>();
        services.AddScoped<ICountryAdapter, CountryAdapter>();
        services.AddScoped<IProvinceAdapter, ProvinceAdapter>();
        services.AddScoped<ICityAdapter, CityAdapter>();

        return services;
    }
}