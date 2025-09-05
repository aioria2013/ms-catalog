using Microsoft.EntityFrameworkCore;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Outbound;
using MsCatalog.Infrastructure.Data;

namespace MsCatalog.Infrastructure.Repositories;

public class GenderRepository : IGenderRepository
{
    private readonly CatalogDbContext _context;

    public GenderRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Gender>> GetAllAsync()
    {
        return await _context.Genders
            .Where(x => x.Status)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Gender?> GetByIdAsync(int id)
    {
        return await _context.Genders
            .FirstOrDefaultAsync(x => x.Id == id && x.Status);
    }

    public async Task<Gender> CreateAsync(Gender gender)
    {
        _context.Genders.Add(gender);
        await _context.SaveChangesAsync();
        return gender;
    }

    public async Task<Gender?> UpdateAsync(int id, Gender gender)
    {
        var existing = await _context.Genders.FindAsync(id);
        if (existing == null) return null;

        existing.Code = gender.Code;
        existing.Name = gender.Name;
        existing.Description = gender.Description;
        existing.Status = gender.Status;
        existing.UpdatedAt = gender.UpdatedAt;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Genders.FindAsync(id);
        if (entity == null) return false;

        entity.Status = false;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
}