using fiap_fase1_tech_challenge.Common.Helpers;
using fiap_fase1_tech_challenge.Modules.Users.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IValidator<UserCreateRequest> _validatorCreate;
    private readonly IValidator<UserUpdateRequest> _validatorUpdate;

    public UserController(IUserService service, IValidator<UserCreateRequest> validatorCreate, IValidator<UserUpdateRequest> validatorUpdate)
    {
        _service = service;
        _validatorCreate = validatorCreate;
        _validatorUpdate = validatorUpdate;
    }

    [Authorize(Policy = "Admin")]
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
    public async Task<IActionResult> Create([FromBody] UserCreateRequest user)
    {
        await ValidationHelper.ValidateAsync(_validatorCreate, user);

        var created = await _service.CreateAsync(user);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    [Authorize(Policy = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserUpdateRequest user)
    {
        await ValidationHelper.ValidateAsync(_validatorUpdate, user);

        var updated = await _service.UpdateAsync(id, user);

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
