using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAppApi.Data;
using TodoAppApi.Entity;
using TodoAppApi.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TodoAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TodosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoToGetDTO>>> GetTodos()
        {
            var todos = await _context.Todos.Include(t => t.Category).ToListAsync();
            var todoDTOs = todos.Select(t => new TodoToGetDTO
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted,
                Category = t.Category
            });

            return Ok(todoDTOs);
        }

        [HttpGet("/category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodosByCategory(int categoryId)
        {
            var todos = await _context.Todos.Where(t => t.CategoryId == categoryId).Include(t => t.Category).ToListAsync();
            var todoDTOs = todos.Select(t => new TodoToGetDTO
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted,
                Category = t.Category
            });

            return Ok(todoDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _context.Todos.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            var todoDTO = new TodoToGetDTO
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted,
                Category = todo.Category
            };

            return Ok(todoDTO);
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> CreateTodo([FromBody] TodoToCreateDTO todoToCreateDTO)
        {
            var todo = new Todo
            {
                Title = todoToCreateDTO.Title,
                IsCompleted = false,
                CategoryId = todoToCreateDTO.CategoryId,
            };

            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(int id, [FromBody] TodoToUpdateDTO todoToUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            todo.Title = todoToUpdateDTO.Title;
            todo.IsCompleted = todoToUpdateDTO.IsCompleted;
            todo.CategoryId = todoToUpdateDTO.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}