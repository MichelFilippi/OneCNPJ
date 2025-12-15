using OneCNPJ.Common.Enums;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.DTOs.VOs.Presentation;

namespace OneCNPJ.DTOs.VOs
{
    public class RegistroSubclasseVO 
        : BaseVO,
        IEntityVO<RegistroSubclasse, RegistroSubclasseVO, RegistroSubclasseListaVO>
    {
        public long CadfiId { get; set; }
        public virtual CadfiVO? Cadfi { get; set; }
        public long? RegistroClasseId { get; set; }
        public virtual RegistroClasse? RegistroClasse { get; set; }
        public List<string> Cabecalho { get; set; } = [];
        public long IdRegistroClasse { get; set; }
        public long IdRegistroSubclasse { get; set; }
        public string? CodigoCvm { get; set; }
        public DateOnly? DataConstituicao { get; set; }
        public DateOnly? DataInicio { get; set; }
        public string DenominacaoSocial { get; set; } = string.Empty;
        public string? Situacao { get; set; }
        public DateOnly? DataInicioSituacao { get; set; }
        public string? FormaCondominio { get; set; }
        public string? Exclusivo { get; set; }
        public string? PublicoAlvo { get; set; } = string.Empty;

        public RegistroSubclasseVO() { }

        public RegistroSubclasseVO FromDomain(RegistroSubclasse model)
        {
            return new RegistroSubclasseVO
            {
                Id = model.Id,
                CadfiId = model.CadfiId,
                RegistroClasseId = model.RegistroClasseId,
                Cabecalho = model.Cabecalho,
                IdRegistroClasse = model.IdRegistroClasse,
                IdRegistroSubclasse = model.IdRegistroSubclasse,
                CodigoCvm = model.CodigoCvm,
                DataConstituicao = model.DataConstituicao,
                DataInicio = model.DataInicio,
                DenominacaoSocial = model.DenominacaoSocial,
                Situacao = model.Situacao,
                DataInicioSituacao = model.DataInicioSituacao,
                FormaCondominio = model.FormaCondominio,
                Exclusivo = model.Exclusivo,
                PublicoAlvo = model.PublicoAlvo,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public RegistroSubclasseListaVO ListFromDomain(RegistroSubclasse model)
        {
            return new RegistroSubclasseListaVO
            {
                Id = model.Id,
                CadfiId = model.CadfiId,
                RegistroClasseId = model.RegistroClasseId,
                IdRegistroClasse = model.IdRegistroClasse,
                IdRegistroSubclasse = model.IdRegistroSubclasse,
                CodigoCvm = model.CodigoCvm,
                DataConstituicao = model.DataConstituicao,
                DataInicio = model.DataInicio,
                DenominacaoSocial = model.DenominacaoSocial,
                Situacao = model.Situacao,
                DataInicioSituacao = model.DataInicioSituacao,
                FormaCondominio = model.FormaCondominio,
                Exclusivo = model.Exclusivo,
                PublicoAlvo = model.PublicoAlvo,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public RegistroSubclasse ToDomain()
        {
            return new RegistroSubclasse
            {
                Id = this.Id,
                CadfiId = this.CadfiId,
                RegistroClasseId = this.RegistroClasseId,
                Cabecalho = this.Cabecalho,
                IdRegistroClasse = this.IdRegistroClasse,
                IdRegistroSubclasse = this.IdRegistroSubclasse,
                CodigoCvm = this.CodigoCvm,
                DataConstituicao = this.DataConstituicao,
                DataInicio = this.DataInicio,
                DenominacaoSocial = this.DenominacaoSocial,
                Situacao = this.Situacao,
                DataInicioSituacao = this.DataInicioSituacao,
                FormaCondominio = this.FormaCondominio,
                Exclusivo = this.Exclusivo,
                PublicoAlvo = this.PublicoAlvo,
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }

        public SubclasseVO ToSubclasse()
        {
            return new SubclasseVO
            {
                CodigoCvm = this.CodigoCvm,
                DataConstituicao = this.DataConstituicao,
                DataInicio = this.DataInicio,
                DenominacaoSocial = this.DenominacaoSocial,
                Situacao = this.Situacao,
                DataInicioSituacao = this.DataInicioSituacao,
                FormaCondominio = this.FormaCondominio,
                Exclusivo = this.Exclusivo,
                PublicoAlvo = this.PublicoAlvo
            };
        }
    }
}
