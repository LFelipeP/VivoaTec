using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using VivoaTec.Models;

namespace VivoaTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/email
        [HttpGet("{Email}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(string Email)
        {
            var todoItem = await _context.TodoItems.FindAsync(Email);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        [HttpPut("{Email}")]
        public async Task<IActionResult> PutTodoItem(string Email, TodoItem todoItem)
        {
            if (Email != todoItem.Email)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(Email))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            Random rnd = new Random();
            todoItem.Cartao = rnd.Next(100000000, 999999999);
        _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { Email = todoItem.Email }, todoItem.Cartao);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{Email}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(string Email)
        {
            var todoItem = await _context.TodoItems.FindAsync(Email);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(string Email)
        {
            return _context.TodoItems.Any(e => e.Email == Email);
        }
    }
}
