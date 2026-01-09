using Microsoft.EntityFrameworkCore;
using OneCNPJ.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneCNPJ.Domain.Models
{
    [Table("cnpj_importacao")]
    public class CnpjImportacao : BaseModel, IEntity
    {
        // Ex: "2025-12-01" (referência dos dados baixados)
        [Column("data_referencia")]
        public DateTime DataReferencia { get; set; }

        // Ex: nome da pasta/versionamento do site RFB (ou string do "último diretório")
        [Column("versao_origem")]
        public string VersaoOrigem { get; set; } = string.Empty;

        // URL base do diretório que você baixou
        [Column("url_origem")]
        public string UrlOrigem { get; set; } = string.Empty;

        // Status geral do lote
        [Column("status")]
        public StatusEnum Status { get; set; } = StatusEnum.Processamento;

        [Column("inicio_em")]
        public DateTime InicioEm { get; set; } = DateTime.UtcNow;

        [Column("fim_em")]
        public DateTime? FimEm { get; set; }

        [Column("mensagem")]
        public string? Mensagem { get; set; }

        public long CnpjEmpresaId { get; set; }

        [ForeignKey(nameof(CnpjEmpresaId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual List<CnpjEmpresa?> CnpjEmpresa { get; set; }

        // Controle por dataset (você pode ajustar o nome conforme seu layout)
        [Column("status_empresas")]
        public StatusEnum StatusEmpresas { get; set; } = StatusEnum.Processamento;

        [Column("linhas_empresas_total")]
        public long LinhasEmpresasTotal { get; set; } = 0;

        [Column("linhas_empresas_importadas")]
        public long LinhasEmpresasImportadas { get; set; } = 0;

        [Column("linhas_empresas_falhas")]
        public long LinhasEmpresasFalhas { get; set; } = 0;

        [Column("status_estabelecimentos")]
        public StatusEnum StatusEstabelecimentos { get; set; } = StatusEnum.Processamento;

        [Column("linhas_estabelecimentos_total")]
        public long LinhasEstabelecimentosTotal { get; set; } = 0;

        [Column("linhas_estabelecimentos_importadas")]
        public long LinhasEstabelecimentosImportadas { get; set; } = 0;

        [Column("linhas_estabelecimentos_falhas")]
        public long LinhasEstabelecimentosFalhas { get; set; } = 0;

        [Column("status_socios")]
        public StatusEnum StatusSocios { get; set; } = StatusEnum.Processamento;

        [Column("linhas_socios_total")]
        public long LinhasSociosTotal { get; set; } = 0;

        [Column("linhas_socios_importadas")]
        public long LinhasSociosImportadas { get; set; } = 0;

        [Column("linhas_socios_falhas")]
        public long LinhasSociosFalhas { get; set; } = 0;

        [Column("status_simples")]
        public StatusEnum StatusSimples { get; set; } = StatusEnum.Processamento;

        [Column("linhas_simples_total")]
        public long LinhasSimplesTotal { get; set; } = 0;

        [Column("linhas_simples_importadas")]
        public long LinhasSimplesImportadas { get; set; } = 0;

        [Column("linhas_simples_falhas")]
        public long LinhasSimplesFalhas { get; set; } = 0;

        // Satélites (opcional, mas recomendo rastrear também)
        [Column("status_satelites")]
        public StatusEnum StatusSatelites { get; set; } = StatusEnum.Processamento;

        [Column("linhas_satelites_importadas")]
        public long LinhasSatelitesImportadas { get; set; } = 0;

        [Column("linhas_satelites_falhas")]
        public long LinhasSatelitesFalhas { get; set; } = 0;
    }
}
