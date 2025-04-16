using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fiap_fase1_tech_challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameLibraryController: ControllerBase
    {
        private readonly IGameLibraryService _service;

        public GameLibraryController(IGameLibraryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var gameLibrary = await _service.GetByIdAsync(id);
            return gameLibrary == null ? NotFound() : Ok(gameLibrary);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GameLibrary gameLibrary)
        {
            var created = await _service.CreateAsync(gameLibrary);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GameLibrary gameLibrary)
        {
            if (id != gameLibrary.Id) return BadRequest();
            var updated = await _service.UpdateAsync(gameLibrary);
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
