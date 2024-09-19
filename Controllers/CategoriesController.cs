using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAppApi.Data;
using TodoAppApi.DTOs;
using TodoAppApi.Entity;

namespace TodoAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryToGetDTO>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            var categoryDTOs = categories.Select(c => new CategoryToGetDTO
            {
                Id = c.Id,
                Name = c.Name
            });

            return Ok(categoryDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryToGetDTO>> GetCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDTO = new CategoryToGetDTO
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(CategoryToCreateDTO categoryToCreateDTO)
        {
            var category = new Category
            {
                Name = categoryToCreateDTO.Name
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryToUpdateDTO categoryToUpdateDTO)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryToUpdateDTO.Name;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}