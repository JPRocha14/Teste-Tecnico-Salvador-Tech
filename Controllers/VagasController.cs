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

        // GET: api/vagas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaga>>> GetVagas()
        {
            return await _context.Vagas.ToListAsync();
        }

        // GET: api/vagas/5
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

        // POST: api/vagas
        [HttpPost]
        public async Task<ActionResult<Vaga>> PostVaga(VagaCreateDTO vagaDto)
        {
            var vaga = new Vaga
            {
                Title = vagaDto.Title,
                Status = vagaDto.Status,
                Created_at = DateTime.Now,  // Gerenciado pelo banco (ou pode ser ajustado)
                Updated_at = DateTime.Now  // Gerenciado automaticamente ao atualizar
            };

            _context.Vagas.Add(vaga);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVaga), new { id = vaga.Id }, vaga);
        }

        // PUT: api/vagas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaga(int id, VagaUpdateDTO vagaDto)
        {
            // Não há necessidade de verificar o ID do DTO, pois o ID da URL já é o único ID válido
            var vaga = await _context.Vagas.FindAsync(id);

            if (vaga == null)
            {
                return NotFound();
            }

            // Atualizando os campos Title e Status com os dados enviados no DTO
            vaga.Title = vagaDto.Title;
            vaga.Status = vagaDto.Status;

            // Atualizando o campo Updated_at com a data e hora atual
            vaga.Updated_at = DateTime.Now;

            // Marca a vaga como modificada
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


        // DELETE: api/vagas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaga(int id)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }

            // Removendo a vaga do contexto e salvando as alterações no banco de dados
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
