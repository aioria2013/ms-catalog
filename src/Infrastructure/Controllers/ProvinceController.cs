using Microsoft.AspNetCore.Mvc;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;

namespace MsCatalog.Infrastructure.Controllers;

[Route("ms-catalog/provinces")]
[ApiController]
public class ProvinceController : ControllerBase
{
    private readonly IProvinceUseCase _useCase;

    public ProvinceController(IProvinceUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Province>>> GetAll([FromQuery] int? countryId)
    {
        if (countryId.HasValue)
        {
            var filteredResult = await _useCase.GetByCountryIdAsync(countryId.Value);
            return Ok(filteredResult);
        }
        
        var result = await _useCase.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Province>> GetById(int id)
    {
        var result = await _useCase.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Province>> Create([FromBody] Province province)
    {
        var result = await _useCase.CreateAsync(province);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Province>> Update(int id, [FromBody] Province province)
    {
        var result = await _useCase.UpdateAsync(id, province);
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

    [HttpGet("{provinceId}/cities")]
    public async Task<ActionResult<IEnumerable<City>>> GetCitiesByProvinceId(int provinceId)
    {
        var result = await _useCase.GetCitiesByProvinceIdAsync(provinceId);
        return Ok(result);
    }
}