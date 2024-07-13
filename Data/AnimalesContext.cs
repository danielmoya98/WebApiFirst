using Microsoft.EntityFrameworkCore;
using WebApiFirst.Models;

namespace WebApiFirst.Data;

public class AnimalesContext : DbContext
{
    public AnimalesContext(DbContextOptions<AnimalesContext> options) : base(options)
    {
    }

    public DbSet<Especie> Especies { get; set; }
    public DbSet<Habitat> Habitats { get; set; }
    public DbSet<Animal> Animales { get; set; }
}