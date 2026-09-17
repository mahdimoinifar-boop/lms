using LmsProject.DTOs.Category;
using LmsProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LmsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories =await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(
            int id)
        {
            var category =await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound("Category not found.");

            return Ok(category);
        }

        [HttpPost]
       [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDto>> Create(
            CreateCategoryDto dto)
        {
            var category = await _categoryService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),new { id = category.CategoryId },category);
        }

        [HttpPut("{id}")]
     [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id,CreateCategoryDto dto)
        {
            var result =
                await _categoryService.UpdateAsync(id,dto);

            if (!result)
                return NotFound("Category not found.");

            return NoContent();
        }

        [HttpDelete("{id}")]
       [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =await _categoryService.DeleteAsync(id);

            if (!result)
                return NotFound("Category not found.");

            return NoContent();
        }
    }
}