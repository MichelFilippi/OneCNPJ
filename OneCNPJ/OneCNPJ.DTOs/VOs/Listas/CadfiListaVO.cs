using OneCNPJ.Common.Enums;

namespace OneCNPJ.DTOs.VOs.Listas
{
    public class CadfiListaVO 
        : IEntityListVO
    {
        public long Id { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Processamento;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        public string Hash { get; set; } = string.Empty;
        public StatusEnum StatusNaoAdaptados175 { get; set; } = StatusEnum.Processamento;
        public StatusEnum StatusRegistroFundo { get; set; } = StatusEnum.Processamento;
        public StatusEnum StatusRegistroClasse { get; set; } = StatusEnum.Processamento;
        public StatusEnum StatusRegistroSubclasse { get; set; } = StatusEnum.Processamento;

        public CadfiListaVO() { }
    }
}
