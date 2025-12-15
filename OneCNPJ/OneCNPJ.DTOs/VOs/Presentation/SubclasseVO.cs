namespace OneCNPJ.DTOs.VOs.Presentation
{
    public class SubclasseVO
    {
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
    }
}
