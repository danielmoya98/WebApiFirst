using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFirst.Data;
using WebApiFirst.Models;

namespace WebApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitatController : ControllerBase
    {
        private readonly AnimalesContext _context;

        public HabitatController(AnimalesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habitat>>> GetHabitats()
        {
            return await _context.Habitats.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Habitat>> GetHabitat(int id)
        {
            var habitat = await _context.Habitats.FindAsync(id);
            if (habitat == null)
            {
                return NotFound();
            }
            return habitat;
        }

        [HttpPost]
        public async Task<ActionResult<Habitat>> PostHabitat(Habitat habitat)
        {
            _context.Habitats.Add(habitat);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHabitat), new { id = habitat.HabitatID }, habitat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabitat(int id, Habitat habitat)
        {
            if (id != habitat.HabitatID)
            {
                return BadRequest();
            }
            _context.Entry(habitat).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Habitats.Any(e => e.HabitatID == id))
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
        public async Task<IActionResult> DeleteHabitat(int id)
        {
            var habitat = await _context.Habitats.FindAsync(id);
            if (habitat == null)
            {
                return NotFound();
            }
            _context.Habitats.Remove(habitat);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
