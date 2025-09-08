using MsCatalog.Domain.Models;

namespace MsCatalog.Domain.Ports.Outbound;

public interface IGenderAdapter
{
    Task<IEnumerable<Gender>> GetAllAsync();
    Task<Gender?> GetByIdAsync(int id);
    Task<Gender> CreateAsync(Gender gender);
    Task<Gender?> UpdateAsync(int id, Gender gender);
    Task<bool> DeleteAsync(int id);
}