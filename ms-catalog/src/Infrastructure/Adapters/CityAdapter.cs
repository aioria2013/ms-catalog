using Microsoft.EntityFrameworkCore;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Outbound;
using MsCatalog.Domain.Models.Context;

namespace MsCatalog.Infrastructure.Adapters;

public class CityAdapter : ICityAdapter
{
    private readonly CatalogDbContext _context;

    public CityAdapter(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _context.Cities
            .Include(x => x.Province)
                .ThenInclude(x => x.Country)
            .Where(x => x.Status)
            .OrderBy(x => x.Province.Country.Name)
            .ThenBy(x => x.Province.Name)
            .ThenBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<City>> GetByProvinceIdAsync(int provinceId)
    {
        return await _context.Cities
            .Include(x => x.Province)
                .ThenInclude(x => x.Country)
            .Where(x => x.ProvinceId == provinceId && x.Status)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<City>> GetByCountryIdAsync(int countryId)
    {
        return await _context.Cities
            .Include(x => x.Province)
                .ThenInclude(x => x.Country)
            .Where(x => x.Province.CountryId == countryId && x.Status)
            .OrderBy(x => x.Province.Name)
            .ThenBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<City?> GetByIdAsync(int id)
    {
        return await _context.Cities
            .Include(x => x.Province)
                .ThenInclude(x => x.Country)
            .FirstOrDefaultAsync(x => x.Id == id && x.Status);
    }

    public async Task<City> CreateAsync(City city)
    {
        _context.Cities.Add(city);
        await _context.SaveChangesAsync();
        return city;
    }

    public async Task<City?> UpdateAsync(int id, City city)
    {
        var existing = await _context.Cities.FindAsync(id);
        if (existing == null) return null;

        existing.ProvinceId = city.ProvinceId;
        existing.Code = city.Code;
        existing.Name = city.Name;
        existing.Status = city.Status;
        existing.UpdatedAt = city.UpdatedAt;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Cities.FindAsync(id);
        if (entity == null) return false;

        entity.Status = false;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
}