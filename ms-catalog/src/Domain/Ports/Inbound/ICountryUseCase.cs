using MsCatalog.Domain.Models;

namespace MsCatalog.Domain.Ports.Inbound;

public interface ICountryUseCase
{
    Task<IEnumerable<Country>> GetAllAsync();
    Task<Country?> GetByIdAsync(int id);
    Task<Country> CreateAsync(Country country);
    Task<Country?> UpdateAsync(int id, Country country);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Province>> GetProvincesByCountryIdAsync(int countryId);
}