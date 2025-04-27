using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fiap_fase1_tech_challenge.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GameController : ControllerBase
  {
    private readonly IGameService _service;

    public GameController(IGameService service)
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
    public async Task<IActionResult> Create([FromBody] GameCreateRequest game)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var created = await _service.CreateAsync(game);
      return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Game game)
    {
      if (id != game.Id) return BadRequest();
      var updated = await _service.UpdateAsync(game);
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
