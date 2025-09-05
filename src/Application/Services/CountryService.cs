using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;
using MsCatalog.Domain.Ports.Outbound;

namespace MsCatalog.Application.Services;

public class CountryService : ICountryUseCase
{
    private readonly ICountryRepository _repository;

    public CountryService(ICountryRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Country>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Country?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<Country> CreateAsync(Country country)
    {
        country.CreatedAt = DateTime.UtcNow;
        return _repository.CreateAsync(country);
    }

    public async Task<Country?> UpdateAsync(int id, Country country)
    {
        country.UpdatedAt = DateTime.UtcNow;
        return await _repository.UpdateAsync(id, country);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }

    public Task<IEnumerable<Province>> GetProvincesByCountryIdAsync(int countryId)
    {
        return _repository.GetProvincesByCountryIdAsync(countryId);
    }
}