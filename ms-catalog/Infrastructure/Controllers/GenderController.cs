using Microsoft.AspNetCore.Mvc;
using MsCatalog.Domain.Models;
using MsCatalog.Domain.Ports.Inbound;

namespace MsCatalog.Infrastructure.Controllers;

[Route("ms-catalog/genders")]
[ApiController]
public class GenderController : ControllerBase
{
    private readonly IGenderUseCase _useCase;

    public GenderController(IGenderUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Gender>>> GetAll()
    {
        var result = await _useCase.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Gender>> GetById(int id)
    {
        var result = await _useCase.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Gender>> Create([FromBody] Gender gender)
    {
        var result = await _useCase.CreateAsync(gender);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Gender>> Update(int id, [FromBody] Gender gender)
    {
        var result = await _useCase.UpdateAsync(id, gender);
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