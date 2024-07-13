using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFirst.Data;
using WebApiFirst.Models;

namespace WebApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalesController : ControllerBase
    {
        private readonly AnimalesContext _context;

        public AnimalesController(AnimalesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimales()
        {
            return await _context.Animales
                .Include(a => a.Especie)
                .Include(a => a.Habitat)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animales
                .Include(a => a.Especie)
                .Include(a => a.Habitat)
                .FirstOrDefaultAsync(a => a.AnimalID == id);

            if (animal == null)
            {
                return NotFound();
            }
            return animal;
        }

        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            _context.Animales.Add(animal);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAnimal), new { id = animal.AnimalID }, animal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.AnimalID)
            {
                return BadRequest();
            }
            _context.Entry(animal).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Animales.Any(e => e.AnimalID == id))
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
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _context.Animales.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            _context.Animales.Remove(animal);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
