using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;
using MsCatalog.Domain.Ports.Outbound;

namespace MsCatalog.Application.Services;

public class IdentificationTypeService : IIdentificationTypeUseCase
{
    private readonly IIdentificationTypeRepository _repository;

    public IdentificationTypeService(IIdentificationTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<IdentificationType>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<IdentificationType?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<IdentificationType> CreateAsync(IdentificationType identificationType)
    {
        identificationType.CreatedAt = DateTime.UtcNow;
        return _repository.CreateAsync(identificationType);
    }

    public async Task<IdentificationType?> UpdateAsync(int id, IdentificationType identificationType)
    {
        identificationType.UpdatedAt = DateTime.UtcNow;
        return await _repository.UpdateAsync(id, identificationType);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}