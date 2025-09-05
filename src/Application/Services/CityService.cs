using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;
using MsCatalog.Domain.Ports.Outbound;

namespace MsCatalog.Application.Services;

public class CityService : ICityUseCase
{
    private readonly ICityRepository _repository;

    public CityService(ICityRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<City>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<IEnumerable<City>> GetByProvinceIdAsync(int provinceId)
    {
        return _repository.GetByProvinceIdAsync(provinceId);
    }

    public Task<IEnumerable<City>> GetByCountryIdAsync(int countryId)
    {
        return _repository.GetByCountryIdAsync(countryId);
    }

    public Task<City?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<City> CreateAsync(City city)
    {
        city.CreatedAt = DateTime.UtcNow;
        return _repository.CreateAsync(city);
    }

    public async Task<City?> UpdateAsync(int id, City city)
    {
        city.UpdatedAt = DateTime.UtcNow;
        return await _repository.UpdateAsync(id, city);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}