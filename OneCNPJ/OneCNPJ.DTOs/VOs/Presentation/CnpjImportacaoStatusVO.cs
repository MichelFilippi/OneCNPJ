using OneCNPJ.Common.Enums;
using OneCNPJ.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs.Presentation
{
    public class CnpjImportacaoStatusVO
    {
        public StatusEnum Status { get; set; } = StatusEnum.Processamento;
        public string StatusDescricao => Status.GetDescription();
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
        public TimeSpan DuracaoProcesso => DataAtualizacao - DataCriacao;
        public StatusEnum StatusNaoAdaptados175 { get; set; } = StatusEnum.Processamento;
        public string StatusNaoAdaptados175Descricao => StatusNaoAdaptados175.GetDescription();
        public StatusEnum StatusRegistroFundo { get; set; } = StatusEnum.Processamento;
        public string StatusRegistroFundoDescricao => StatusRegistroFundo.GetDescription();
        public StatusEnum StatusRegistroClasse { get; set; } = StatusEnum.Processamento;
        public string StatusRegistroClasseDescricao => StatusRegistroClasse.GetDescription();
        public StatusEnum StatusRegistroSubclasse { get; set; } = StatusEnum.Processamento;
        public string StatusRegistroSubclasseDescricao => StatusRegistroSubclasse.GetDescription();
    }
}
