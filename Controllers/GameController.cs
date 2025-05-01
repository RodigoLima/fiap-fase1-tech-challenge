using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fiap_fase1_tech_challenge.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
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
                var game = await _service.GetByIdAsync(id);
                return game == null ? NotFound() : Ok(game);
            }
            [HttpPost]
            public async Task<IActionResult> Create([FromBody] Game game)
            {
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
