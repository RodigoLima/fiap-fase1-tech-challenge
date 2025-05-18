using fiap_fase1_tech_challenge.Common.Helpers;
using fiap_fase1_tech_challenge.Modules.Users.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Users.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;
    private readonly IValidator<RoleCreateRequest> _validatorCreate;
    private readonly IValidator<RoleUpdateRequest> _validatorUpdate;

    public RoleController(IRoleService service, IValidator<RoleCreateRequest> validatorCreate, IValidator<RoleUpdateRequest> validatorUpdate)
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
        var role = await _service.GetByIdAsync(id);
        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoleCreateRequest role)
    {
        await ValidationHelper.ValidateAsync(_validatorCreate, role);

        var created = await _service.CreateAsync(role);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoleUpdateRequest role)
    {
        await ValidationHelper.ValidateAsync(_validatorUpdate, role);

        await _service.UpdateAsync(id, role);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
