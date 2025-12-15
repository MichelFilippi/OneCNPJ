using OneCNPJ.Common.Enums;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.DTOs.VOs.Presentation;

namespace OneCNPJ.DTOs.VOs
{
    public class RegistroFundoVO
        : BaseVO,
        IEntityVO<RegistroFundo, RegistroFundoVO, RegistroFundoListaVO>
    {
        public long CadfiId { get; set; }
        public virtual CadfiVO? Cadfi { get; set; }
        public List<string> Cabecalho { get; set; } = [];
        public long IdRegistroFundo { get; set; }
        public string CnpjFundo { get; set; } = string.Empty;
        public string? CodigoCvm { get; set; }
        public DateOnly? DataRegistro { get; set; }
        public DateOnly? DataConstituicao { get; set; }
        public string TipoFundo { get; set; } = string.Empty;
        public string DenominacaoSocial { get; set; } = string.Empty;
        public DateOnly? DataCancelamento { get; set; }
        public string? Situacao { get; set; }
        public DateOnly? DataInicioSituacao { get; set; }
        public DateOnly? DataAdaptacaoRcvm175 { get; set; }
        public DateOnly? DataInicioExercicioSocial { get; set; }
        public DateOnly? DataFimExercicioSocial { get; set; }
        public decimal PatrimonioLiquido { get; set; } = 0;
        public DateOnly? DataPatrimonioLiquido { get; set; }
        public string? Diretor { get; set; } = string.Empty;
        public string? CnpjAdministrador { get; set; } = string.Empty;
        public string? Administrador { get; set; } = string.Empty;
        public string? TipoPessoaGestor { get; set; }
        public string? DocumentoGestor { get; set; } = string.Empty;
        public string? Gestor { get; set; } = string.Empty;
        public List<RegistroClasseVO> RegistroClasses { get; set; } = [];

        public static CadfiOrigemEnum CadfiOrigem => CadfiOrigemEnum.FundosClassesSubclasses;

        public RegistroFundoVO() { }

        public RegistroFundoVO FromDomain(RegistroFundo model)
        {
            return new RegistroFundoVO
            {
                Id = model.Id,                
                CadfiId = model.CadfiId,
                Cabecalho = model.Cabecalho,
                IdRegistroFundo = model.IdRegistroFundo,
                CnpjFundo = model.CnpjFundo,
                CodigoCvm = model.CodigoCvm,
                DataRegistro = model.DataRegistro,
                DataConstituicao = model.DataConstituicao,
                TipoFundo = model.TipoFundo,
                DenominacaoSocial = model.DenominacaoSocial,
                DataCancelamento = model.DataCancelamento,
                Situacao = model.Situacao,
                DataInicioSituacao = model.DataInicioSituacao,
                DataAdaptacaoRcvm175 = model.DataAdaptacaoRcvm175,
                DataInicioExercicioSocial = model.DataInicioExercicioSocial,
                DataFimExercicioSocial = model.DataFimExercicioSocial,
                PatrimonioLiquido = model.PatrimonioLiquido,
                DataPatrimonioLiquido = model.DataPatrimonioLiquido,
                Diretor = model.Diretor,
                CnpjAdministrador = model.CnpjAdministrador,
                Administrador = model.Administrador,
                TipoPessoaGestor = model.TipoPessoaGestor,
                DocumentoGestor = model.DocumentoGestor,
                Gestor = model.Gestor,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public RegistroFundoListaVO ListFromDomain(RegistroFundo model)
        {
            return new RegistroFundoListaVO
            {
                Id = model.Id,
                CadfiId = model.CadfiId,
                CnpjFundo = model.CnpjFundo,
                CodigoCvm = model.CodigoCvm,
                DataRegistro = model.DataRegistro,
                DataConstituicao = model.DataConstituicao,
                TipoFundo = model.TipoFundo,
                DenominacaoSocial = model.DenominacaoSocial,
                DataCancelamento = model.DataCancelamento,
                Situacao = model.Situacao,
                DataInicioSituacao = model.DataInicioSituacao,
                DataAdaptacaoRcvm175 = model.DataAdaptacaoRcvm175,
                DataInicioExercicioSocial = model.DataInicioExercicioSocial,
                DataFimExercicioSocial = model.DataFimExercicioSocial,
                PatrimonioLiquido = model.PatrimonioLiquido,
                DataPatrimonioLiquido = model.DataPatrimonioLiquido,
                Diretor = model.Diretor,
                CnpjAdministrador = model.CnpjAdministrador,
                Administrador = model.Administrador,
                TipoPessoaGestor = model.TipoPessoaGestor,
                DocumentoGestor = model.DocumentoGestor,
                Gestor = model.Gestor,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public RegistroFundo ToDomain()
        {
            return new RegistroFundo
            {
                Id = this.Id,                
                CadfiId = this.CadfiId,
                Cabecalho = this.Cabecalho,
                IdRegistroFundo = this.IdRegistroFundo,
                CnpjFundo = this.CnpjFundo,
                CodigoCvm = this.CodigoCvm,
                DataRegistro = this.DataRegistro,
                DataConstituicao = this.DataConstituicao,
                TipoFundo = this.TipoFundo,
                DenominacaoSocial = this.DenominacaoSocial,
                DataCancelamento = this.DataCancelamento,
                Situacao = this.Situacao,
                DataInicioSituacao = this.DataInicioSituacao,
                DataAdaptacaoRcvm175 = this.DataAdaptacaoRcvm175,
                DataInicioExercicioSocial = this.DataInicioExercicioSocial,
                DataFimExercicioSocial = this.DataFimExercicioSocial,
                PatrimonioLiquido = this.PatrimonioLiquido,
                DataPatrimonioLiquido = this.DataPatrimonioLiquido,
                Diretor = this.Diretor,
                CnpjAdministrador = this.CnpjAdministrador,
                Administrador = this.Administrador,
                TipoPessoaGestor = this.TipoPessoaGestor,
                DocumentoGestor = this.DocumentoGestor,
                Gestor = this.Gestor,
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }

        public FundoVO ToFundo()
        {
            return new FundoVO
            {
                CnpjFundo = this.CnpjFundo,
                CodigoCvm = this.CodigoCvm,
                DataRegistro = this.DataRegistro,
                DataConstituicao = this.DataConstituicao,
                TipoFundo = this.TipoFundo,
                DenominacaoSocial = this.DenominacaoSocial,
                DataCancelamento = this.DataCancelamento,
                Situacao = this.Situacao,
                DataInicioSituacao = this.DataInicioSituacao,
                DataAdaptacaoRcvm175 = this.DataAdaptacaoRcvm175,
                DataInicioExercicioSocial = this.DataInicioExercicioSocial,
                DataFimExercicioSocial = this.DataFimExercicioSocial,
                PatrimonioLiquido = this.PatrimonioLiquido,
                DataPatrimonioLiquido = this.DataPatrimonioLiquido,
                Diretor = this.Diretor,
                CnpjAdministrador = this.CnpjAdministrador,
                Administrador = this.Administrador,
                TipoPessoaGestor = this.TipoPessoaGestor,
                DocumentoGestor = this.DocumentoGestor,
                Gestor = this.Gestor,
            };  
        }
    }
}
