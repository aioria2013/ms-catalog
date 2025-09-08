using Microsoft.AspNetCore.Mvc;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;

namespace MsCatalog.Infrastructure.Controllers;

[Route("ms-catalog/countries")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICountryUseCase _useCase;

    public CountryController(ICountryUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetAll()
    {
        var result = await _useCase.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetById(int id)
    {
        var result = await _useCase.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Country>> Create([FromBody] Country country)
    {
        var result = await _useCase.CreateAsync(country);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Country>> Update(int id, [FromBody] Country country)
    {
        var result = await _useCase.UpdateAsync(id, country);
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

    [HttpGet("{countryId}/provinces")]
    public async Task<ActionResult<IEnumerable<Province>>> GetProvincesByCountryId(int countryId)
    {
        var result = await _useCase.GetProvincesByCountryIdAsync(countryId);
        return Ok(result);
    }
}