using Microsoft.EntityFrameworkCore;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Outbound;
using MsCatalog.Domain.Models.Context;

namespace MsCatalog.Infrastructure.Adapters;

public class CountryAdapter : ICountryAdapter
{
    private readonly CatalogDbContext _context;

    public CountryAdapter(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Country>> GetAllAsync()
    {
        return await _context.Countries
            .Where(x => x.Status)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Country?> GetByIdAsync(int id)
    {
        return await _context.Countries
            .FirstOrDefaultAsync(x => x.Id == id && x.Status);
    }

    public async Task<Country> CreateAsync(Country country)
    {
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();
        return country;
    }

    public async Task<Country?> UpdateAsync(int id, Country country)
    {
        var existing = await _context.Countries.FindAsync(id);
        if (existing == null) return null;

        existing.Code = country.Code;
        existing.Name = country.Name;
        existing.IsoCode = country.IsoCode;
        existing.Status = country.Status;
        existing.UpdatedAt = country.UpdatedAt;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Countries.FindAsync(id);
        if (entity == null) return false;

        entity.Status = false;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Province>> GetProvincesByCountryIdAsync(int countryId)
    {
        return await _context.Provinces
            .Where(x => x.CountryId == countryId && x.Status)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}