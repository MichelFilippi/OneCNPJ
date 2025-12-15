using OneCNPJ.Common.Enums;

namespace OneCNPJ.DTOs.VOs.Presentation
{
    public class FundoVO
    {
        public CadfiOrigemEnum CadfiOrigem { get; set; } = CadfiOrigemEnum.FundosClassesSubclasses;
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
        public decimal TaxaPerformance { get; set; } = 0;
        public string? TaxaPerformanceInfo { get; set; } = string.Empty;
        public decimal TaxaAdministracao { get; set; } = 0;
        public string? TaxaAdministracaoInfo { get; set; } = string.Empty;

        public List<ClasseVO> Classes { get; set; } = [];
    }
}
