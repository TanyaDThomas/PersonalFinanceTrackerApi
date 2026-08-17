using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Application.Contracts;
using PersonalFinanceTracker.Application.DTOs;

namespace PersonalFinanceTracker.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryCommandService _commandService;
        private readonly ICategoryQueryService _queryService;

        public CategoryController(ICategoryCommandService commandService, ICategoryQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoryList = await _queryService.GetAllAsync();
            return Ok(categoryList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoryById = await _queryService.GetByIdAsync(id);
            return Ok(categoryById);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var category = await _commandService.CreateAsync(dto);

            return CreatedAtAction(
              nameof(GetById),
              new { id = category.Id },
              category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        {
            var updatedCategory = await _commandService.UpdateAsync(id, dto);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commandService.DeleteAsync(id);
            return NoContent();
        }
    }
}
