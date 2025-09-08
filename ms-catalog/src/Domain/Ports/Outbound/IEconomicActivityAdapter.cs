using MsCatalog.Domain.Models;

namespace MsCatalog.Domain.Ports.Outbound;

public interface IEconomicActivityAdapter
{
    Task<IEnumerable<EconomicActivity>> GetAllAsync();
    Task<EconomicActivity?> GetByIdAsync(int id);
    Task<EconomicActivity> CreateAsync(EconomicActivity economicActivity);
    Task<EconomicActivity?> UpdateAsync(int id, EconomicActivity economicActivity);
    Task<bool> DeleteAsync(int id);
}