using Microsoft.EntityFrameworkCore;
using OneCNPJ.Domain;
using OneCNPJ.Domain.Models;
using OneCNPJ.Domain.Models.Satelites;

namespace OneCNPJ.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Raiz / lote
        public DbSet<CnpjImportacao> CnpjImportacoes => Set<CnpjImportacao>();

        // Core
        public DbSet<CnpjEmpresa> CnpjEmpresas => Set<CnpjEmpresa>();
        public DbSet<CnpjEstabelecimento> CnpjEstabelecimentos => Set<CnpjEstabelecimento>();
        public DbSet<CnpjEstabelecimentoCnaeSecundario> CnpjEstabCnaeSec => Set<CnpjEstabelecimentoCnaeSecundario>();
        public DbSet<CnpjSocio> CnpjSocios => Set<CnpjSocio>();
        public DbSet<CnpjSimples> CnpjSimples => Set<CnpjSimples>();

        // Saídas / logs
        public DbSet<Conteudo> Conteudos => Set<Conteudo>();
        public DbSet<Falha> Falhas => Set<Falha>();
        public DbSet<Ignorado> Ignorados => Set<Ignorado>();

        // Satélites
        public DbSet<Cnae> Cnaes => Set<Cnae>();
        public DbSet<Municipio> Municipios => Set<Municipio>();
        public DbSet<Pais> Paises => Set<Pais>();
        public DbSet<NaturezaJuridica> NaturezasJuridicas => Set<NaturezaJuridica>();
        public DbSet<QualificacaoSocio> QualificacoesSocio => Set<QualificacaoSocio>();
        public DbSet<MotivoSituacaoCadastral> MotivosSituacaoCadastral => Set<MotivoSituacaoCadastral>();

        // Se você realmente usa Layout/LayoutCampo no banco
        public DbSet<Layout> Layouts => Set<Layout>();
        public DbSet<LayoutCampo> LayoutCampos => Set<LayoutCampo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NaturezaJuridica>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<QualificacaoSocio>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Cnae>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Municipio>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Pais>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<MotivoSituacaoCadastral>()
                .Property(p => p.Id)
                .ValueGeneratedNever();
        }
    }
}
