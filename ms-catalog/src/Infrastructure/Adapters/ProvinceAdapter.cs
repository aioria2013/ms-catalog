using Microsoft.EntityFrameworkCore;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Outbound;
using MsCatalog.Domain.Models.Context;

namespace MsCatalog.Infrastructure.Adapters;

public class ProvinceAdapter : IProvinceAdapter
{
    private readonly CatalogDbContext _context;

    public ProvinceAdapter(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Province>> GetAllAsync()
    {
        return await _context.Provinces
            .Include(x => x.Country)
            .Where(x => x.Status)
            .OrderBy(x => x.Country.Name)
            .ThenBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Province>> GetByCountryIdAsync(int countryId)
    {
        return await _context.Provinces
            .Include(x => x.Country)
            .Where(x => x.CountryId == countryId && x.Status)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Province?> GetByIdAsync(int id)
    {
        return await _context.Provinces
            .Include(x => x.Country)
            .FirstOrDefaultAsync(x => x.Id == id && x.Status);
    }

    public async Task<Province> CreateAsync(Province province)
    {
        _context.Provinces.Add(province);
        await _context.SaveChangesAsync();
        return province;
    }

    public async Task<Province?> UpdateAsync(int id, Province province)
    {
        var existing = await _context.Provinces.FindAsync(id);
        if (existing == null) return null;

        existing.CountryId = province.CountryId;
        existing.Code = province.Code;
        existing.Name = province.Name;
        existing.Status = province.Status;
        existing.UpdatedAt = province.UpdatedAt;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Provinces.FindAsync(id);
        if (entity == null) return false;

        entity.Status = false;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<City>> GetCitiesByProvinceIdAsync(int provinceId)
    {
        return await _context.Cities
            .Where(x => x.ProvinceId == provinceId && x.Status)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}