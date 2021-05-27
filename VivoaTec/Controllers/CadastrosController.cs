using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        // GET: api/Cadastros/5
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

        // PUT: api/Cadastros/Email@test.com
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
                    return NotFound();
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
                Random rnd = new Random();
                cadastro.Cartao = rnd.Next(100000000, 999999999);
                _context.Cadastros.Add(cadastro);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCadastro", new { Email = cadastro.Email }, cadastro.Cartao);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Cadastros/5
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
