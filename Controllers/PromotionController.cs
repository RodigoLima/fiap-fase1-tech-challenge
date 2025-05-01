using fiap_fase1_tech_challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fiap_fase1_tech_challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _service;
        public PromotionController(IPromotionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var promotions = await _service.GetAllAsync();
            return Ok(promotions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var promotion = await _service.GetByIdAsync(id);
            return promotion == null ? NotFound() : Ok(promotion);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Promotion promotion)
        {
            var created = await _service.CreateAsync(promotion);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.Promotion promotion)
        {
            if (id != promotion.Id) return BadRequest();
            var updated = await _service.UpdateAsync(promotion);
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
