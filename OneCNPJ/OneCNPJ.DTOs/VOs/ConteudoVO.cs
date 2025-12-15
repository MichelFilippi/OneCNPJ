using OneCNPJ.Common.Enums;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.DTOs.VOs.Presentation;

namespace OneCNPJ.DTOs.VOs
{
    public class ConteudoVO
        : BaseVO,
        IEntityVO<Conteudo, ConteudoVO, ConteudoListaVO>
    {
        public long CadfiId { get; set; }
        public virtual CadfiVO? Cadfi { get; set; }
        public List<string> Cabecalho { get; set; } = [];
        public string TipoFundo { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string DenominacaoSocial { get; set; } = string.Empty;
        public DateOnly? DataRegistro { get; set; }
        public DateOnly? DataConstituicao { get; set; }
        public string? CodigoCvm { get; set; }
        public DateOnly? DataCancelamento { get; set; }
        public string? Situacao { get; set; }
        public DateOnly? SituacaoDataInicial { get; set; }
        public DateOnly? AtividadeDataInicial { get; set; }
        public DateOnly? ExercicioDataInicial { get; set; }
        public DateOnly? ExercicioDataFinal { get; set; }
        public string? Classe { get; set; }
        public DateOnly? ClasseDataInicial { get; set; }
        public string? Rentabilidade { get; set; }
        public string? Condominio { get; set; }
        public string? Cotas { get; set; }
        public string? Exclusivo { get; set; }
        public string? TributacaoLongoPrazo { get; set; } = string.Empty;
        public string? PublicoAlvo { get; set; } = string.Empty;
        public string? EntidadeInvestimento { get; set; }
        public decimal TaxaPerformance { get; set; } = 0;
        public string? TaxaPerformanceInfo { get; set; } = string.Empty;
        public decimal TaxaAdministracao { get; set; } = 0;
        public string? TaxaAdministracaoInfo { get; set; } = string.Empty;
        public decimal PatrimonioLiquido { get; set; } = 0;
        public DateOnly? PatrimonioLiquidoData { get; set; }
        public string? Diretor { get; set; } = string.Empty;
        public string? CnpjAdministrador { get; set; } = string.Empty;
        public string? Administrador { get; set; } = string.Empty;
        public string? TipoGestor { get; set; }
        public string? DocumentoGestor { get; set; } = string.Empty;
        public string? Gestor { get; set; } = string.Empty;
        public string? CnpjAuditor { get; set; } = string.Empty;
        public string? Auditor { get; set; } = string.Empty;
        public string? CnpjCustodiante { get; set; } = string.Empty;
        public string? Custodiante { get; set; } = string.Empty;
        public string? CnpjControlador { get; set; } = string.Empty;
        public string? Controlador { get; set; } = string.Empty;
        public string? AplicarTotalExterior { get; set; }
        public string? ClasseAnbima { get; set; } = string.Empty;

        public static CadfiOrigemEnum CadfiOrigem => CadfiOrigemEnum.NaoAdaptados175;

        public ConteudoVO() { }

        public ConteudoVO FromDomain(Conteudo model)
        {
            return new ConteudoVO
            {
                Id = model.Id,
                CadfiId = model.CadfiId,
                Cabecalho = model.Cabecalho,
                TipoFundo = model.TipoFundo,
                Cnpj = model.Cnpj,
                DenominacaoSocial = model.DenominacaoSocial,
                DataRegistro = model.DataRegistro,
                DataConstituicao = model.DataConstituicao,
                CodigoCvm = model.CodigoCvm,
                DataCancelamento = model.DataCancelamento,
                Situacao = model.Situacao,
                SituacaoDataInicial = model.SituacaoDataInicial,
                AtividadeDataInicial = model.AtividadeDataInicial,
                ExercicioDataInicial = model.ExercicioDataInicial,
                ExercicioDataFinal = model.ExercicioDataFinal,
                Classe = model.Classe,
                ClasseDataInicial = model.ClasseDataInicial,
                Rentabilidade = model.Rentabilidade,
                Condominio = model.Condominio,
                Cotas = model.Cotas,
                Exclusivo = model.Exclusivo,
                TributacaoLongoPrazo = model.TributacaoLongoPrazo,
                PublicoAlvo = model.PublicoAlvo,
                EntidadeInvestimento = model.EntidadeInvestimento,
                TaxaPerformance = model.TaxaPerformance,
                TaxaPerformanceInfo = model.TaxaPerformanceInfo,
                TaxaAdministracao = model.TaxaAdministracao,
                TaxaAdministracaoInfo = model.TaxaAdministracaoInfo,
                PatrimonioLiquido = model.PatrimonioLiquido,
                PatrimonioLiquidoData = model.PatrimonioLiquidoData,
                Diretor = model.Diretor,
                CnpjAdministrador = model.CnpjAdministrador,
                Administrador = model.Administrador,
                TipoGestor = model.TipoGestor,
                DocumentoGestor = model.DocumentoGestor,
                Gestor = model.Gestor,
                CnpjAuditor = model.CnpjAuditor,
                Auditor = model.Auditor,
                CnpjCustodiante = model.CnpjCustodiante,
                Custodiante = model.Custodiante,
                CnpjControlador = model.CnpjControlador,
                Controlador = model.Controlador,
                AplicarTotalExterior = model.AplicarTotalExterior,
                ClasseAnbima = model.ClasseAnbima,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public ConteudoListaVO ListFromDomain(Conteudo model)
        {
            return new ConteudoListaVO
            {
                Id = model.Id,
                CadfiId = model.CadfiId,
                TipoFundo = model.TipoFundo,
                Cnpj = model.Cnpj,
                DenominacaoSocial = model.DenominacaoSocial,
                DataRegistro = model.DataRegistro,
                DataConstituicao = model.DataConstituicao,
                CodigoCvm = model.CodigoCvm,
                DataCancelamento = model.DataCancelamento,
                Situacao = model.Situacao,
                SituacaoDataInicial = model.SituacaoDataInicial,
                AtividadeDataInicial = model.AtividadeDataInicial,
                ExercicioDataInicial = model.ExercicioDataInicial,
                ExercicioDataFinal = model.ExercicioDataFinal,
                Classe = model.Classe,
                ClasseDataInicial = model.ClasseDataInicial,
                Rentabilidade = model.Rentabilidade,
                Condominio = model.Condominio,
                Cotas = model.Cotas,
                Exclusivo = model.Exclusivo,
                TributacaoLongoPrazo = model.TributacaoLongoPrazo,
                PublicoAlvo = model.PublicoAlvo,
                EntidadeInvestimento = model.EntidadeInvestimento,
                TaxaPerformance = model.TaxaPerformance,
                TaxaPerformanceInfo = model.TaxaPerformanceInfo,
                TaxaAdministracao = model.TaxaAdministracao,
                TaxaAdministracaoInfo = model.TaxaAdministracaoInfo,
                PatrimonioLiquido = model.PatrimonioLiquido,
                PatrimonioLiquidoData = model.PatrimonioLiquidoData,
                Diretor = model.Diretor,
                CnpjAdministrador = model.CnpjAdministrador,
                Administrador = model.Administrador,
                TipoGestor = model.TipoGestor,
                DocumentoGestor = model.DocumentoGestor,
                Gestor = model.Gestor,
                CnpjAuditor = model.CnpjAuditor,
                Auditor = model.Auditor,
                CnpjCustodiante = model.CnpjCustodiante,
                Custodiante = model.Custodiante,
                CnpjControlador = model.CnpjControlador,
                Controlador = model.Controlador,
                AplicarTotalExterior = model.AplicarTotalExterior,
                ClasseAnbima = model.ClasseAnbima,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public Conteudo ToDomain()
        {
            return new Conteudo
            {
                Id = this.Id,
                CadfiId = this.CadfiId,
                Cabecalho = this.Cabecalho,
                TipoFundo = this.TipoFundo,
                Cnpj = this.Cnpj,
                DenominacaoSocial = this.DenominacaoSocial,
                DataRegistro = this.DataRegistro,
                DataConstituicao = this.DataConstituicao,
                CodigoCvm = this.CodigoCvm,
                DataCancelamento = this.DataCancelamento,
                Situacao = this.Situacao,
                SituacaoDataInicial = this.SituacaoDataInicial,
                AtividadeDataInicial = this.AtividadeDataInicial,
                ExercicioDataInicial = this.ExercicioDataInicial,
                ExercicioDataFinal = this.ExercicioDataFinal,
                Classe = this.Classe,
                ClasseDataInicial = this.ClasseDataInicial,
                Rentabilidade = this.Rentabilidade,
                Condominio = this.Condominio,
                Cotas = this.Cotas,
                Exclusivo = this.Exclusivo,
                TributacaoLongoPrazo = this.TributacaoLongoPrazo ?? string.Empty,
                PublicoAlvo = this.PublicoAlvo ?? string.Empty,
                EntidadeInvestimento = this.EntidadeInvestimento ?? string.Empty,
                TaxaPerformance = this.TaxaPerformance,
                TaxaPerformanceInfo = this.TaxaPerformanceInfo ?? string.Empty,
                TaxaAdministracao = this.TaxaAdministracao,
                TaxaAdministracaoInfo = this.TaxaAdministracaoInfo ?? string.Empty,
                PatrimonioLiquido = this.PatrimonioLiquido,
                PatrimonioLiquidoData = this.PatrimonioLiquidoData,
                Diretor = this.Diretor ?? string.Empty,
                CnpjAdministrador = this.CnpjAdministrador ?? string.Empty,
                Administrador = this.Administrador ?? string.Empty,
                TipoGestor = this.TipoGestor ?? string.Empty,
                DocumentoGestor = this.DocumentoGestor ?? string.Empty,
                Gestor = this.Gestor ?? string.Empty,
                CnpjAuditor = this.CnpjAuditor ?? string.Empty,
                Auditor = this.Auditor ?? string.Empty,
                CnpjCustodiante = this.CnpjCustodiante ?? string.Empty,
                Custodiante = this.Custodiante ?? string.Empty,
                CnpjControlador = this.CnpjControlador ?? string.Empty,
                Controlador = this.Controlador ?? string.Empty,
                AplicarTotalExterior = this.AplicarTotalExterior ?? string.Empty,
                ClasseAnbima = this.ClasseAnbima ?? string.Empty,
                Status = this.Status
            };
        }

        public FundoVO ToFundo()
        {
            var fundo = new FundoVO()
            {
                CnpjFundo = this.Cnpj,
                TipoFundo = this.TipoFundo,
                DenominacaoSocial = this.DenominacaoSocial,
                DataRegistro = this.DataRegistro,
                DataConstituicao = this.DataConstituicao,
                CodigoCvm = this.CodigoCvm,
                DataCancelamento = this.DataCancelamento,
                Situacao = this.Situacao,
                DataInicioSituacao = this.SituacaoDataInicial,
                DataInicioExercicioSocial = this.ExercicioDataInicial,
                DataFimExercicioSocial = this.ExercicioDataInicial,
                PatrimonioLiquido = this.PatrimonioLiquido,
                DataPatrimonioLiquido = this.PatrimonioLiquidoData,
                Diretor = this.Diretor,
                CnpjAdministrador = this.CnpjAdministrador,
                Administrador = this.Administrador,
                DocumentoGestor = this.DocumentoGestor,
                Gestor = this.Gestor,
                TaxaPerformance = this.TaxaPerformance,
                TaxaPerformanceInfo = this.TaxaPerformanceInfo,
                TaxaAdministracao = this.TaxaAdministracao,
                TaxaAdministracaoInfo = this.TaxaAdministracaoInfo,
            };

            var classe = new ClasseVO()
            {
                CnpjClasse = this.Cnpj,
                TipoClasse = this.Classe,
                DataInicio = this.ClasseDataInicial,
                ClasseCotas = this.Cotas,
                TributacaoLongoPrazo = this.TributacaoLongoPrazo,
                Exclusivo = this.Exclusivo,
                PublicoAlvo = this.PublicoAlvo,
                EntidadeInvestimento = this.EntidadeInvestimento,
                IndicadorDesempenho = this.Rentabilidade,
                FormaCondominio = this.Condominio,
                CnpjAuditor = this.CnpjAuditor,
                Auditor = this.Auditor,
                CnpjCustodiante = this.CnpjCustodiante,
                Custodiante = this.Custodiante,
                PermitidoAplicacaoCemPorCentoExterior = this.AplicarTotalExterior,
                ClasseAnbima = this.ClasseAnbima,
            };

            fundo.Classes = [.. fundo.Classes, classe];
            return fundo;
        }
    }
}
