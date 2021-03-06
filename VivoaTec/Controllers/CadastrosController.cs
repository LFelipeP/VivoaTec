using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivoaTec.Models;
using VivoaTec.Tools;

namespace VivoaTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastrosController : ControllerBase
    {
        private readonly CadastroContext _context;

        public CadastrosController(CadastroContext context)
        {
            _context = context;
        }

        // GET: api/Cadastros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cadastro>>> GetCadastros()
        {
            return await _context.Cadastros.ToListAsync();
        }

        // GET: api/Cadastros/email@test.com
        [HttpGet("{Email}")]
        public async Task<ActionResult<Cadastro>> GetCadastro(string Email)
        {
            var cadastro = await _context.Cadastros.FindAsync(Email);

            if (cadastro == null)
            {
                return NotFound();
            }

            return cadastro;
        }

        // PUT: api/Cadastros/email@test.com
        [HttpPut("{Email}")]
        public async Task<IActionResult> PutCadastro(string Email, Cadastro cadastro)
        {
            if (Email != cadastro.Email)
            {
                return BadRequest();
            }

            _context.Entry(cadastro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CadastroExists(Email))
                {
                    return NotFound("Cadastro não encontrado");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cadastros
        [HttpPost]
        public async Task<ActionResult<Cadastro>> PostCadastro(Cadastro cadastro)
        {
            if (EmailVerify.IsValidEmail(cadastro.Email))
            {
                if (_context.Cadastros.Find(cadastro.Email) == null) {
                    cadastro.Cartao = CardGenerator.CreateCard();
                    _context.Cadastros.Add(cadastro);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetCadastro", new { Email = cadastro.Email }, cadastro.Cartao);
                }
                else
                {
                    return BadRequest("E-mail ja utilizado");
                }
            }
            else
            {
                return BadRequest("E-mail inválido");
            }
        }

        // DELETE: api/Cadastros/email@test.com
        [HttpDelete("{Email}")]
        public async Task<ActionResult<Cadastro>> DeleteCadastro(string Email)
        {
            var cadastro = await _context.Cadastros.FindAsync(Email);
            if (cadastro == null)
            {
                return NotFound();
            }

            _context.Cadastros.Remove(cadastro);
            await _context.SaveChangesAsync();

            return cadastro;
        }

        private bool CadastroExists(string Email)
        {
            return _context.Cadastros.Any(e => e.Email == Email);
        }
    }
}
