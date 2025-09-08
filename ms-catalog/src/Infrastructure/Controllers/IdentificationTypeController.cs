using Microsoft.AspNetCore.Mvc;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;

namespace MsCatalog.Infrastructure.Controllers;

[Route("ms-catalog/identification-types")]
[ApiController]
public class IdentificationTypeController : ControllerBase
{
    private readonly IIdentificationTypeUseCase _useCase;

    public IdentificationTypeController(IIdentificationTypeUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IdentificationType>>> GetAll()
    {
        var result = await _useCase.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IdentificationType>> GetById(int id)
    {
        var result = await _useCase.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<IdentificationType>> Create([FromBody] IdentificationType identificationType)
    {
        var result = await _useCase.CreateAsync(identificationType);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IdentificationType>> Update(int id, [FromBody] IdentificationType identificationType)
    {
        var result = await _useCase.UpdateAsync(id, identificationType);
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