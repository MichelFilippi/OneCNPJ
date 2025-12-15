using Microsoft.EntityFrameworkCore;
using OneCNPJ.Domain.Models;
using OneCNPJ.Domain.Models.Satelites;

namespace OneCNPJ.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cadfi> Cadfis { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        public DbSet<Falha> Falhas { get; set; }
        public DbSet<Ignorado> Ignorados { get; set; }
        public DbSet<RegistroClasse> RegistroClasses { get; set; }
        public DbSet<RegistroFundo> RegistroFundos { get; set; }
        public DbSet<RegistroSubclasse> RegistroSubclasses { get; set; }

        public DbSet<Layout> Layouts { get; set; }
        public DbSet<LayoutCampo> LayoutCampos { get; set; }
    }
}
