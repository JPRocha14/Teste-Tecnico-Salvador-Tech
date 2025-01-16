using Microsoft.EntityFrameworkCore;
using VagasAPI.Models;

namespace VagasAPI.Models
{
    public class VagasContext : DbContext
    {
        public VagasContext(DbContextOptions<VagasContext> options) : base(options) { }

        public DbSet<Vaga> Vagas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura a propriedade Created_at para ter o valor padrão como a data atual
            modelBuilder.Entity<Vaga>()
                .Property(v => v.Created_at)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();  // Garante que a data seja gerada ao adicionar a vaga

            // Configura a propriedade Updated_at para ser atualizada automaticamente sempre que a vaga for atualizada
            modelBuilder.Entity<Vaga>()
                .Property(v => v.Updated_at)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();  // Atualiza a data quando a vaga for modificada
        }
    }
}
