using fiap_fase1_tech_challenge.DTOs.Game;
using fiap_fase1_tech_challenge.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _service;
    private readonly IValidator<GameCreateRequest> _validatorCreate;
    private readonly IValidator<GameUpdateRequest> _validatorUpdate;

    public GameController(IGameService service, IValidator<GameCreateRequest> validatorCreate, IValidator<GameUpdateRequest> validatorUpdate)
    {
        _service = service;
        _validatorCreate = validatorCreate;
        _validatorUpdate = validatorUpdate;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _service.GetByIdAsync(id);
        return Ok(user);
    }
    [Authorize(Policy = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GameCreateRequest game)
    {
        await ValidationHelper.ValidateAsync(_validatorCreate, game);

        var created = await _service.CreateAsync(game);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    [Authorize(Policy = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] GameUpdateRequest game)
    {
        await ValidationHelper.ValidateAsync(_validatorUpdate, game);

        var updated = await _service.UpdateAsync(id, game);

        return NoContent();
    }
    [Authorize(Policy = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return NoContent();
    }
}
