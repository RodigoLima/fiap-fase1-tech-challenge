using fiap_fase1_tech_challenge.Modules.GamesLibrary.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.GamesLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class GameLibraryController : ControllerBase
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
    public async Task<IActionResult> Create([FromBody] GameLibraryCreateRequest gameLibrary)
    {
        var created = await _service.CreateAsync(gameLibrary);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] GameLibraryUpdateRequest gameLibrary)
    {

        if (gameLibrary == null)
            return BadRequest("Dados inválidos.");

        var updated = await _service.UpdateAsync(id, gameLibrary);

        return updated
            ? NoContent()
            : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
