using Microsoft.EntityFrameworkCore;
using Pandora.Models;
namespace Pandora.DbContext;
using Microsoft.EntityFrameworkCore;
using Pandora.Models;



public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

public DbSet<Pandora.Models.Product> Product { get; set; } = default!;

public DbSet<Pandora.Models.Material> Material { get; set; } = default!;

public DbSet<Pandora.Models.Category> Category { get; set; } = default!;
    }

