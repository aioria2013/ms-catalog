using Microsoft.EntityFrameworkCore;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Outbound;
using MsCatalog.Domain.Models.Context;

namespace MsCatalog.Infrastructure.Adapters;

public class IdentificationTypeAdapter : IIdentificationTypeAdapter
{
    private readonly CatalogDbContext _context;

    public IdentificationTypeAdapter(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IdentificationType>> GetAllAsync()
    {
        return await _context.IdentificationTypes
            .Where(x => x.Status)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<IdentificationType?> GetByIdAsync(int id)
    {
        return await _context.IdentificationTypes
            .FirstOrDefaultAsync(x => x.Id == id && x.Status);
    }

    public async Task<IdentificationType> CreateAsync(IdentificationType identificationType)
    {
        _context.IdentificationTypes.Add(identificationType);
        await _context.SaveChangesAsync();
        return identificationType;
    }

    public async Task<IdentificationType?> UpdateAsync(int id, IdentificationType identificationType)
    {
        var existing = await _context.IdentificationTypes.FindAsync(id);
        if (existing == null) return null;

        existing.Code = identificationType.Code;
        existing.Name = identificationType.Name;
        existing.Description = identificationType.Description;
        existing.Status = identificationType.Status;
        existing.UpdatedAt = identificationType.UpdatedAt;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.IdentificationTypes.FindAsync(id);
        if (entity == null) return false;

        entity.Status = false;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
}