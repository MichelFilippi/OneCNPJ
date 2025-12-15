using OneCNPJ.Common.Enums;
using OneCNPJ.Common.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain.Models
{
    [Table("cadfi")]
    public class Cadfi : BaseModel, IEntity
    {
        [Column("hash")]
        [Required]
        [MaxLength(200)] 
        public string Hash { get; set; } = General.GenerateSalt(30);

        [Column("status_nao_adaptados_175")]
        public StatusEnum StatusNaoAdaptados175 { get; set; } = StatusEnum.Processamento;

        [Column("linhas_nao_adaptados_175")]
        public int LinhasNaoAdaptados175 { get; set; } = 0;

        [Column("linhas_importadas_nao_adaptados_175")]
        public int LinhasImportadasNaoAdaptados175 { get; set; } = 0;

        [Column("linhas_falhas_nao_adaptados_175")]
        public int LinhasFalhasNaoAdaptados175 { get; set; } = 0;

        [Column("linhas_ignoradas_nao_adaptados_175")]
        public int LinhasIgnoradasNaoAdaptados175 { get; set; } = 0;

        [Column("linhas_com_erros_nao_adaptados_175")]
        public List<int> LinhasComErrosNaoAdaptados175 { get; set; } = [];

        [Column("status_registro_fundo")]
        public StatusEnum StatusRegistroFundo { get; set; } = StatusEnum.Processamento;

        [Column("linhas_registro_fundo")]
        public int LinhasRegistroFundo { get; set; } = 0;

        [Column("linhas_importadas_registro_fundo")]
        public int LinhasImportadasRegistroFundo { get; set; } = 0;

        [Column("linhas_falhas_registro_fundo")]
        public int LinhasFalhasRegistroFundo { get; set; } = 0;

        [Column("linhas_ignoradas_registro_fundo")]
        public int LinhasIgnoradasRegistroFundo { get; set; } = 0;

        [Column("linhas_com_erros_registro_fundo")]
        public List<int> LinhasComErrosRegistroFundo { get; set; } = [];

        [Column("status_registro_classe")]
        public StatusEnum StatusRegistroClasse { get; set; } = StatusEnum.Processamento;

        [Column("linhas_registro_classe")]
        public int LinhasRegistroClasse { get; set; } = 0;

        [Column("linhas_importadas_registro_classe")]
        public int LinhasImportadasRegistroClasse { get; set; } = 0;

        [Column("linhas_falhas_registro_classe")]
        public int LinhasFalhasRegistroClasse { get; set; } = 0;

        [Column("linhas_ignoradas_registro_classe")]
        public int LinhasIgnoradasRegistroClasse { get; set; } = 0;

        [Column("linhas_com_erros_registro_classe")]
        public List<int> LinhasComErrosRegistroClasse { get; set; } = [];

        [Column("status_registro_subclasse")]
        public StatusEnum StatusRegistroSubclasse { get; set; } = StatusEnum.Processamento;

        [Column("linhas_registro_subclasse")]
        public int LinhasRegistroSubclasse { get; set; } = 0;

        [Column("linhas_importadas_registro_subclasse")]
        public int LinhasImportadasRegistroSubclasse { get; set; } = 0;

        [Column("linhas_falhas_registro_subclasse")]
        public int LinhasFalhasRegistroSubclasse { get; set; } = 0;

        [Column("linhas_ignoradas_registro_subclasse")]
        public int LinhasIgnoradasRegistroSubclasse { get; set; } = 0;

        [Column("linhas_com_erros_registro_subclasse")]
        public List<int> LinhasComErrosRegistroSubclasse { get; set; } = [];

        public Cadfi() { }
    }
}
