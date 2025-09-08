using MsCatalog.Domain.Models;

namespace MsCatalog.Domain.Ports.Outbound;

public interface ICityAdapter
{
    Task<IEnumerable<City>> GetAllAsync();
    Task<IEnumerable<City>> GetByProvinceIdAsync(int provinceId);
    Task<IEnumerable<City>> GetByCountryIdAsync(int countryId);
    Task<City?> GetByIdAsync(int id);
    Task<City> CreateAsync(City city);
    Task<City?> UpdateAsync(int id, City city);
    Task<bool> DeleteAsync(int id);
}