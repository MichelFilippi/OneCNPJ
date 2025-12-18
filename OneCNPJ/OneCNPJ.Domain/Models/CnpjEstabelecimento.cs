
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OneCNPJ.Domain.Models.Satelites;

namespace OneCNPJ.Domain.Models
{
    [Table("cnpj_estabelecimento")]
    public class CnpjEstabelecimento : BaseModel, IEntity
    {
        [Column("cnpj_basico")]
        public long CnpjBasico { get; set; }

        [Column("cnpj_empresa")]
        public long CnpjEmpresaId { get; set; }

        [ForeignKey(nameof(CnpjEmpresaId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual CnpjEmpresa? CnpjEmpresa { get; set; }

        [Column("cnpj_ordem")]
        public string CnpjOrdem { get; set; } = string.Empty; // 4 dígitos

        [Column("cnpj_dv")]
        public string CnpjDv { get; set; } = string.Empty; // 2 dígitos

        [NotMapped]
        public string Cnpj => $"{CnpjEmpreseId}{CnpjOrdem}{CnpjDv}"; // conveniência

        [Column("identificador_matriz_filial")]
        public string IdentificadorMatrizFilial { get; set; } = string.Empty; // 1=matriz 2=filial

        [Column("nome_fantasia")]
        public string? NomeFantasia { get; set; }

        [Column("situacao_cadastral")]
        public string SituacaoCadastral { get; set; } = string.Empty; // 01,02,03,04,08

        [Column("data_situacao_cadastral")]
        public DateTime? DataSituacaoCadastral { get; set; }

        [Column("motivo_situacao_cadastral_id")]
        public long MotivoSituacaoCadastralId { get; set; } 

        [ForeignKey(nameof(MotivoSituacaoCadastralId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual MotivoSituacaoCadastral? MotivoSituacaoCadastral { get; set; }

        [Column("nome_cidade_exterior")]
        public string? NomeCidadeExterior { get; set; }

        [Column("pais_id")]
        public long? PaisId { get; set; }

        [ForeignKey(nameof(PaisId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Pais? Pais { get; set; }

        [Column("data_inicio_atividade")]
        public DateTime? DataInicioAtividade { get; set; }

        [Column("cnae_principal_id")]
        public long CnaePrincipalId { get; set; } 

        [ForeignKey(nameof(CnaePrincipalId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Cnae? CnaePrincipal { get; set; }

        public virtual List<CnpjEstabelecimentoCnaeSecundario> CnaesSecundarios { get; set; } = [];

        // ENDEREÇO (campos já no estabelecimento, mas com FK p/ município)
        [Column("tipo_logradouro")]
        public string? TipoLogradouro { get; set; }

        [Column("logradouro")]
        public string? Logradouro { get; set; }

        [Column("numero")]
        public string? Numero { get; set; }

        [Column("complemento")]
        public string? Complemento { get; set; }

        [Column("bairro")]
        public string? Bairro { get; set; }

        [Column("cep")]
        public string? Cep { get; set; }

        [Column("uf")]
        public string? Uf { get; set; }

        [Column("municipio_id")]
        public long MunicipioId { get; set; } 

        [ForeignKey(nameof(MunicipioId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Municipio? Municipio { get; set; }

        public CnpjEstabelecimento() { }
    }

}