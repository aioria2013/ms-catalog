using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;
using MsCatalog.Domain.Ports.Outbound;

namespace MsCatalog.Application.Services;

public class GenderService : IGenderUseCase
{
    private readonly IGenderRepository _repository;

    public GenderService(IGenderRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Gender>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Gender?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<Gender> CreateAsync(Gender gender)
    {
        gender.CreatedAt = DateTime.UtcNow;
        return _repository.CreateAsync(gender);
    }

    public async Task<Gender?> UpdateAsync(int id, Gender gender)
    {
        gender.UpdatedAt = DateTime.UtcNow;
        return await _repository.UpdateAsync(id, gender);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}