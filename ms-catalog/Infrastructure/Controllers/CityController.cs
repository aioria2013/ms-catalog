using Microsoft.AspNetCore.Mvc;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;

namespace MsCatalog.Infrastructure.Controllers;

[Route("ms-catalog/cities")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly ICityUseCase _useCase;

    public CityController(ICityUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetAll([FromQuery] int? provinceId, [FromQuery] int? countryId)
    {
        if (provinceId.HasValue)
        {
            var provinceResult = await _useCase.GetByProvinceIdAsync(provinceId.Value);
            return Ok(provinceResult);
        }
        
        if (countryId.HasValue)
        {
            var countryResult = await _useCase.GetByCountryIdAsync(countryId.Value);
            return Ok(countryResult);
        }
        
        var result = await _useCase.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetById(int id)
    {
        var result = await _useCase.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<City>> Create([FromBody] City city)
    {
        var result = await _useCase.CreateAsync(city);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<City>> Update(int id, [FromBody] City city)
    {
        var result = await _useCase.UpdateAsync(id, city);
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _useCase.DeleteAsync(id);
        if (!result)
            return NotFound();
        
        return NoContent();
    }
}