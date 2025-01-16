using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VagasAPI.Models;
using VagasAPI.DTOs; // Importando o namespace para os DTOs
using System.Linq;
using System.Threading.Tasks;
using System;

namespace VagasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VagasController : ControllerBase
    {
        private readonly VagasContext _context;

        public VagasController(VagasContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaga>>> GetVagas()
        {
            return await _context.Vagas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vaga>> GetVaga(int id)
        {
            var vaga = await _context.Vagas.FindAsync(id);

            if (vaga == null)
            {
                return NotFound();
            }

            return vaga;
        }

        [HttpPost]
        public async Task<ActionResult<Vaga>> PostVaga(VagaCreateDTO vagaDto)
        {
            var vaga = new Vaga
            {
                Title = vagaDto.Title,
                Status = vagaDto.Status,
                Created_at = DateTime.Now, 
                Updated_at = DateTime.Now
            };

            _context.Vagas.Add(vaga);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVaga), new { id = vaga.Id }, vaga);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaga(int id, VagaUpdateDTO vagaDto)
        {
            var vaga = await _context.Vagas.FindAsync(id);

            if (vaga == null)
            {
                return NotFound();
            }

            vaga.Title = vagaDto.Title;
            vaga.Status = vagaDto.Status;

            vaga.Updated_at = DateTime.Now;

            _context.Entry(vaga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VagaExists(id))
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaga(int id)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }

            _context.Vagas.Remove(vaga);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VagaExists(int id)
        {
            return _context.Vagas.Any(e => e.Id == id);
        }
    }
}
