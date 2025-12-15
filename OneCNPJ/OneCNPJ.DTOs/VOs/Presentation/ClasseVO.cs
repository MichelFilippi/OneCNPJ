namespace OneCNPJ.DTOs.VOs.Presentation
{
    public class ClasseVO
    {
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
        public List<SubclasseVO> Subclasses { get; set; } = [];
    }
}
