using Microsoft.EntityFrameworkCore;
using OneCNPJ.Domain;
using OneCNPJ.Domain.Models;
using OneCNPJ.Domain.Models.Satelites;

namespace OneCNPJ.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //public DbSet<Cnpj> Cnpj { get; set; }
        //public DbSet<Conteudo> Conteudos { get; set; }
        //public DbSet<Falha> Falhas { get; set; }
        //public DbSet<Ignorado> Ignorados { get; set; }
        //public DbSet<RegistroClasse> RegistroClasses { get; set; }
        //public DbSet<RegistroFundo> RegistroFundos { get; set; }
        //public DbSet<RegistroSubclasse> RegistroSubclasses { get; set; }

        //public DbSet<Layout> Layouts { get; set; }
        //public DbSet<LayoutCampo> LayoutCampos { get; set; }

        public DbSet<CnpjEmpresa> CnpjEmpresas => Set<CnpjEmpresa>();
        public DbSet<NaturezaJuridica> NaturezasJuridicas => Set<NaturezaJuridica>();
        public DbSet<QualificacaoSocio> QualificacoesSocio => Set<QualificacaoSocio>();
        public DbSet<CnpjSocio> CnpjSocios => Set<CnpjSocio>();
        public DbSet<CnpjSimples> CnpjSimples => Set<CnpjSimples>();
        public DbSet<CnpjEstabelecimento> CnpjEstabelecimentos => Set<CnpjEstabelecimento>();
    }
}
