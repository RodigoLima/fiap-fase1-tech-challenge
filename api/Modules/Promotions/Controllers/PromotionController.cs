using fiap_fase1_tech_challenge.Common.Helpers;
using fiap_fase1_tech_challenge.Modules.Promotions.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Promotions.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PromotionController : ControllerBase
{
    private readonly IPromotionService _service;
    private readonly IValidator<PromotionCreateRequest> _validatorCreate;
    private readonly IValidator<PromotionUpdateRequest> _validatorUpdate;

    public PromotionController(IPromotionService service, IValidator<PromotionCreateRequest> validatorCreate, IValidator<PromotionUpdateRequest> validatorUpdate)
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
    public async Task<IActionResult> Create([FromBody] PromotionCreateRequest Promotion)
    {
        await ValidationHelper.ValidateAsync(_validatorCreate, Promotion);

        var created = await _service.CreateAsync(Promotion);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    [Authorize(Policy = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PromotionUpdateRequest promotion)
    {
        await ValidationHelper.ValidateAsync(_validatorUpdate, promotion);

        var updated = await _service.UpdateAsync(id, promotion);

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

