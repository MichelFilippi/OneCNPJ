using OneCNPJ.Common.Enums;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.DTOs.VOs.Presentation;

namespace OneCNPJ.DTOs.VOs
{
    public class RegistroClasseVO
        : BaseVO,
        IEntityVO<RegistroClasse, RegistroClasseVO, RegistroClasseListaVO>
    {
        public long CadfiId { get; set; }
        public virtual CadfiVO? Cadfi { get; set; }
        public long? RegistroFundoId { get; set; }
        public virtual RegistroFundo? RegistroFundo { get; set; }
        public List<string> Cabecalho { get; set; } = [];
        public long IdRegistroFundo { get; set; }
        public long IdRegistroClasse { get; set; }
        public string CnpjClasse { get; set; } = string.Empty;
        public string? CodigoCvm { get; set; }
        public DateOnly? DataRegistro { get; set; }
        public DateOnly? DataConstituicao { get; set; }
        public DateOnly? DataInicio { get; set; }
        public string? TipoClasse { get; set; }
        public string DenominacaoSocial { get; set; } = string.Empty;
        public string? Situacao { get; set; }
        public DateOnly? DataInicioSituacao { get; set; }
        public string? Classificacao { get; set; }
        public string? IndicadorDesempenho { get; set; }
        public string? ClasseCotas { get; set; }
        public string? ClasseAnbima { get; set; }
        public string? TributacaoLongoPrazo { get; set; }
        public string? EntidadeInvestimento { get; set; }
        public string? PermitidoAplicacaoCemPorCentoExterior { get; set; }
        public string? ClasseEsg { get; set; }
        public string? FormaCondominio { get; set; }
        public string? Exclusivo { get; set; }
        public string? PublicoAlvo { get; set; }
        public decimal? PatrimonioLiquido { get; set; }
        public DateOnly? DataPatrimonioLiquido { get; set; }
        public string? CnpjAuditor { get; set; }
        public string? Auditor { get; set; }
        public string? CnpjCustodiante { get; set; }
        public string? Custodiante { get; set; }
        public string? CnpjControlador { get; set; }
        public string? Controlador { get; set; }
        public List<RegistroSubclasseVO> RegistroSubclasses { get; set; } = [];

        public RegistroClasseVO() { }

        public RegistroClasseVO FromDomain(RegistroClasse model)
        {
            return new RegistroClasseVO
            {
                Id = model.Id,
                CadfiId = model.CadfiId,
                RegistroFundoId = model.RegistroFundoId,
                Cabecalho = model.Cabecalho,
                IdRegistroFundo = model.IdRegistroFundo,
                IdRegistroClasse = model.IdRegistroClasse,
                CnpjClasse = model.CnpjClasse,
                CodigoCvm = model.CodigoCvm,
                DataRegistro = model.DataRegistro,
                DataConstituicao = model.DataConstituicao,
                DataInicio = model.DataInicio,
                TipoClasse = model.TipoClasse,
                DenominacaoSocial = model.DenominacaoSocial,
                Situacao = model.Situacao,
                DataInicioSituacao = model.DataInicioSituacao,
                Classificacao = model.Classificacao,
                IndicadorDesempenho = model.IndicadorDesempenho,
                ClasseCotas = model.ClasseCotas,
                ClasseAnbima = model.ClasseAnbima,
                TributacaoLongoPrazo = model.TributacaoLongoPrazo,
                EntidadeInvestimento = model.EntidadeInvestimento,
                PermitidoAplicacaoCemPorCentoExterior = model.PermitidoAplicacaoCemPorCentoExterior,
                ClasseEsg = model.ClasseEsg,
                FormaCondominio = model.FormaCondominio,
                Exclusivo = model.Exclusivo,
                PublicoAlvo = model.PublicoAlvo,
                PatrimonioLiquido = model.PatrimonioLiquido,
                DataPatrimonioLiquido = model.DataPatrimonioLiquido,
                CnpjAuditor = model.CnpjAuditor,
                Auditor = model.Auditor,
                CnpjCustodiante = model.CnpjCustodiante,
                Custodiante = model.Custodiante,
                CnpjControlador = model.CnpjControlador,
                Controlador = model.Controlador,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public RegistroClasseListaVO ListFromDomain(RegistroClasse model)
        {
            return new RegistroClasseListaVO
            {
                Id = model.Id,
                CadfiId = model.CadfiId,
                Cabecalho = model.Cabecalho,
                RegistroFundoId = model.RegistroFundoId,
                IdRegistroFundo = model.IdRegistroFundo,
                IdRegistroClasse = model.IdRegistroClasse,
                CnpjClasse = model.CnpjClasse,
                CodigoCvm = model.CodigoCvm,
                DataRegistro = model.DataRegistro,
                DataConstituicao = model.DataConstituicao,
                DataInicio = model.DataInicio,
                TipoClasse = model.TipoClasse,
                DenominacaoSocial = model.DenominacaoSocial,
                Situacao = model.Situacao,
                DataInicioSituacao = model.DataInicioSituacao,
                Classificacao = model.Classificacao,
                IndicadorDesempenho = model.IndicadorDesempenho,
                ClasseCotas = model.ClasseCotas,
                ClasseAnbima = model.ClasseAnbima,
                TributacaoLongoPrazo = model.TributacaoLongoPrazo,
                EntidadeInvestimento = model.EntidadeInvestimento,
                PermitidoAplicacaoCemPorCentoExterior = model.PermitidoAplicacaoCemPorCentoExterior,
                ClasseEsg = model.ClasseEsg,
                FormaCondominio = model.FormaCondominio,
                Exclusivo = model.Exclusivo,
                PublicoAlvo = model.PublicoAlvo,
                PatrimonioLiquido = model.PatrimonioLiquido,
                DataPatrimonioLiquido = model.DataPatrimonioLiquido,
                CnpjAuditor = model.CnpjAuditor,
                Auditor = model.Auditor,
                CnpjCustodiante = model.CnpjCustodiante,
                Custodiante = model.Custodiante,
                CnpjControlador = model.CnpjControlador,
                Controlador = model.Controlador,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public RegistroClasse ToDomain()
        {
            return new RegistroClasse
            {
                Id = this.Id,
                CadfiId = this.CadfiId,
                RegistroFundoId = this.RegistroFundoId,
                Cabecalho = this.Cabecalho,
                IdRegistroFundo = this.IdRegistroFundo,
                IdRegistroClasse = this.IdRegistroClasse,
                CnpjClasse = this.CnpjClasse,
                CodigoCvm = this.CodigoCvm,
                DataRegistro = this.DataRegistro,
                DataConstituicao = this.DataConstituicao,
                DataInicio = this.DataInicio,
                TipoClasse = this.TipoClasse,
                DenominacaoSocial = this.DenominacaoSocial,
                Situacao = this.Situacao,
                DataInicioSituacao = this.DataInicioSituacao,
                Classificacao = this.Classificacao,
                IndicadorDesempenho = this.IndicadorDesempenho,
                ClasseCotas = this.ClasseCotas,
                ClasseAnbima = this.ClasseAnbima,
                TributacaoLongoPrazo = this.TributacaoLongoPrazo,
                EntidadeInvestimento = this.EntidadeInvestimento,
                PermitidoAplicacaoCemPorCentoExterior = this.PermitidoAplicacaoCemPorCentoExterior,
                ClasseEsg = this.ClasseEsg,
                FormaCondominio = this.FormaCondominio,
                Exclusivo = this.Exclusivo,
                PublicoAlvo = this.PublicoAlvo,
                PatrimonioLiquido = this.PatrimonioLiquido,
                DataPatrimonioLiquido = this.DataPatrimonioLiquido,
                CnpjAuditor = this.CnpjAuditor,
                Auditor = this.Auditor,
                CnpjCustodiante = this.CnpjCustodiante,
                Custodiante = this.Custodiante,
                CnpjControlador = this.CnpjControlador,
                Controlador = this.Controlador,
                Status = this.Status
            };
        }

        public ClasseVO ToClasse()
        {
            return new ClasseVO
            {
                CnpjClasse = this.CnpjClasse,
                CodigoCvm = this.CodigoCvm,
                DenominacaoSocial = this.DenominacaoSocial,
                TipoClasse = this.TipoClasse,
                Situacao = this.Situacao,
                DataInicioSituacao = this.DataInicioSituacao,
                Classificacao = this.Classificacao,
                IndicadorDesempenho = this.IndicadorDesempenho,
                ClasseCotas = this.ClasseCotas,
                ClasseAnbima = this.ClasseAnbima,
                TributacaoLongoPrazo = this.TributacaoLongoPrazo,
                EntidadeInvestimento = this.EntidadeInvestimento,
                PermitidoAplicacaoCemPorCentoExterior = this.PermitidoAplicacaoCemPorCentoExterior,
                ClasseEsg = this.ClasseEsg,
                FormaCondominio = this.FormaCondominio,
                Exclusivo = this.Exclusivo,
                PublicoAlvo = this.PublicoAlvo,
                PatrimonioLiquido = this.PatrimonioLiquido ?? 0,
                DataPatrimonioLiquido = this.DataPatrimonioLiquido,
                Auditor = this.Auditor,
                CnpjAuditor = this.CnpjAuditor,
                Custodiante = this.Custodiante,
                CnpjCustodiante = this.CnpjCustodiante,
                Controlador = this.Controlador,
                CnpjControlador = this.CnpjControlador,
            };
        }
    }
}
