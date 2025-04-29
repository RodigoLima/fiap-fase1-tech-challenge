using fiap_fase1_tech_challenge.DTOs.Promotion;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fiap_fase1_tech_challenge.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PromotionController : ControllerBase
  {
    private readonly IPromotionService _service;

    public PromotionController(IPromotionService service)
    {
      _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var user = await _service.GetByIdAsync(id);
      return user == null ? NotFound() : Ok(user);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PromotionCreateRequest Promotion)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var created = await _service.CreateAsync(Promotion);
      return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Promotion Promotion)
    {
      if (id != Promotion.Id) return BadRequest();
      var updated = await _service.UpdateAsync(Promotion);
      return updated ? NoContent() : NotFound();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var deleted = await _service.DeleteAsync(id);
      return deleted ? NoContent() : NotFound();
    }
  }
}
