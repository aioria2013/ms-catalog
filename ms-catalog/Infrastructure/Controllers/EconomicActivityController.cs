using Microsoft.AspNetCore.Mvc;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;

namespace MsCatalog.Infrastructure.Controllers;

[Route("ms-catalog/economic-activities")]
[ApiController]
public class EconomicActivityController : ControllerBase
{
    private readonly IEconomicActivityUseCase _useCase;

    public EconomicActivityController(IEconomicActivityUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EconomicActivity>>> GetAll()
    {
        var result = await _useCase.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EconomicActivity>> GetById(int id)
    {
        var result = await _useCase.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<EconomicActivity>> Create([FromBody] EconomicActivity economicActivity)
    {
        var result = await _useCase.CreateAsync(economicActivity);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EconomicActivity>> Update(int id, [FromBody] EconomicActivity economicActivity)
    {
        var result = await _useCase.UpdateAsync(id, economicActivity);
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