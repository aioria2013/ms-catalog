using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;
using MsCatalog.Domain.Ports.Outbound;

namespace MsCatalog.Application.Services;

public class ProvinceService : IProvinceUseCase
{
    private readonly IProvinceRepository _repository;

    public ProvinceService(IProvinceRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Province>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<IEnumerable<Province>> GetByCountryIdAsync(int countryId)
    {
        return _repository.GetByCountryIdAsync(countryId);
    }

    public Task<Province?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<Province> CreateAsync(Province province)
    {
        province.CreatedAt = DateTime.UtcNow;
        return _repository.CreateAsync(province);
    }

    public async Task<Province?> UpdateAsync(int id, Province province)
    {
        province.UpdatedAt = DateTime.UtcNow;
        return await _repository.UpdateAsync(id, province);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }

    public Task<IEnumerable<City>> GetCitiesByProvinceIdAsync(int provinceId)
    {
        return _repository.GetCitiesByProvinceIdAsync(provinceId);
    }
}