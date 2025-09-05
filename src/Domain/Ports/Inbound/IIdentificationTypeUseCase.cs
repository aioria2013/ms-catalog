using MsCatalog.Domain.Models;

namespace MsCatalog.Domain.Ports.Inbound;

public interface IIdentificationTypeUseCase
{
    Task<IEnumerable<IdentificationType>> GetAllAsync();
    Task<IdentificationType?> GetByIdAsync(int id);
    Task<IdentificationType> CreateAsync(IdentificationType identificationType);
    Task<IdentificationType?> UpdateAsync(int id, IdentificationType identificationType);
    Task<bool> DeleteAsync(int id);
}