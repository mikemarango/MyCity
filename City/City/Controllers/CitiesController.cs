using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using City.Data;
using City.Models;
using City.DTOs;

namespace City.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CityContext _context;

        public CitiesController(CityContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
            var citiDto = _context.Cities.Select(c => new CityDto
            {
                ID = c.ID,
                Name = c.Name,
                Description = c.Description
            });

            return await citiDto.ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCiti(int id, bool includeAttraction)
        {

            var citi = await _context.Cities.FindAsync(id);

            if (citi == null)
            {
                return NotFound();
            }

            var citiDto = new CityDto()
            {
                ID = citi.ID,
                Name = citi.Name,
                Description = citi.Description
            };

            return citiDto;
        }

        // PUT: api/Cities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCiti(int id, Citi citi)
        {
            if (id != citi.ID)
            {
                return BadRequest();
            }

            _context.Entry(citi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitiExists(id))
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

        // POST: api/Cities
        [HttpPost]
        public async Task<ActionResult<Citi>> PostCiti(Citi citi)
        {
            _context.Cities.Add(citi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCiti", new { id = citi.ID }, citi);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Citi>> DeleteCiti(int id)
        {
            var citi = await _context.Cities.FindAsync(id);
            if (citi == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(citi);
            await _context.SaveChangesAsync();

            return citi;
        }

        private bool CitiExists(int id)
        {
            return _context.Cities.Any(e => e.ID == id);
        }
    }
}
