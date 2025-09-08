using MsCatalog.Domain.Models;

namespace MsCatalog.Domain.Ports.Outbound;

public interface IProvinceAdapter
{
    Task<IEnumerable<Province>> GetAllAsync();
    Task<IEnumerable<Province>> GetByCountryIdAsync(int countryId);
    Task<Province?> GetByIdAsync(int id);
    Task<Province> CreateAsync(Province province);
    Task<Province?> UpdateAsync(int id, Province province);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<City>> GetCitiesByProvinceIdAsync(int provinceId);
}