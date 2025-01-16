using Microsoft.EntityFrameworkCore;
using VagasAPI.Models;

namespace VagasAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Vaga> Vagas { get; set; }
    }
}
