using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;
using MsCatalog.Domain.Ports.Outbound;

namespace MsCatalog.Application.Services;

public class EconomicActivityService : IEconomicActivityUseCase
{
    private readonly IEconomicActivityRepository _repository;

    public EconomicActivityService(IEconomicActivityRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<EconomicActivity>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<EconomicActivity?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<EconomicActivity> CreateAsync(EconomicActivity economicActivity)
    {
        economicActivity.CreatedAt = DateTime.UtcNow;
        return _repository.CreateAsync(economicActivity);
    }

    public async Task<EconomicActivity?> UpdateAsync(int id, EconomicActivity economicActivity)
    {
        economicActivity.UpdatedAt = DateTime.UtcNow;
        return await _repository.UpdateAsync(id, economicActivity);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}