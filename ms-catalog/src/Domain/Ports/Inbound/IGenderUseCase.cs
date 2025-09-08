using MsCatalog.Domain.Models;

namespace MsCatalog.Domain.Ports.Inbound;

public interface IGenderUseCase
{
    Task<IEnumerable<Gender>> GetAllAsync();
    Task<Gender?> GetByIdAsync(int id);
    Task<Gender> CreateAsync(Gender gender);
    Task<Gender?> UpdateAsync(int id, Gender gender);
    Task<bool> DeleteAsync(int id);
}