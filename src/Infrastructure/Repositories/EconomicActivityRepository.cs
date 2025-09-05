using Microsoft.EntityFrameworkCore;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Outbound;
using MsCatalog.Infrastructure.Data;

namespace MsCatalog.Infrastructure.Repositories;

public class EconomicActivityRepository : IEconomicActivityRepository
{
    private readonly CatalogDbContext _context;

    public EconomicActivityRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EconomicActivity>> GetAllAsync()
    {
        return await _context.EconomicActivities
            .Where(x => x.Status)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<EconomicActivity?> GetByIdAsync(int id)
    {
        return await _context.EconomicActivities
            .FirstOrDefaultAsync(x => x.Id == id && x.Status);
    }

    public async Task<EconomicActivity> CreateAsync(EconomicActivity economicActivity)
    {
        _context.EconomicActivities.Add(economicActivity);
        await _context.SaveChangesAsync();
        return economicActivity;
    }

    public async Task<EconomicActivity?> UpdateAsync(int id, EconomicActivity economicActivity)
    {
        var existing = await _context.EconomicActivities.FindAsync(id);
        if (existing == null) return null;

        existing.Code = economicActivity.Code;
        existing.Name = economicActivity.Name;
        existing.Description = economicActivity.Description;
        existing.Status = economicActivity.Status;
        existing.UpdatedAt = economicActivity.UpdatedAt;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.EconomicActivities.FindAsync(id);
        if (entity == null) return false;

        entity.Status = false;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
}