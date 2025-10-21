using Microsoft.EntityFrameworkCore;
using ExpressoData.Domain.Warehouse;

namespace ExpressoData.Infrastructure.Persistence
{
    public class ExpressoDbContext : DbContext
    {
        public ExpressoDbContext(DbContextOptions<ExpressoDbContext> options) : base(options) { }

        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<Bebidas> Bebidas { get; set; }
        public DbSet<Frente> Frentes { get; set; }
        public DbSet<Cozinha> Cozinhas { get; set; }
        public DbSet<Sobremesas> Sobremesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais podem ser feitas aqui
        }
    }
}
