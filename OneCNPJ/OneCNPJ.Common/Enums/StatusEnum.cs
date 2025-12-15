using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OneCNPJ.Common.Enums
{
    public enum StatusEnum
    {
        [Description("Novo")]
        [Display(Name = "Novo")]
        Novo = 0,

        [Description("Aguardando análise")]
        [Display(Name = "Aguardando análise")]
        AguardandoAnalise = 1,

        [Description("Em processamento")]
        [Display(Name = "Em processamento")]
        Processamento = 2,

        [Description("Cadastrado")]
        [Display(Name = "Cadastrado")]
        Cadastrado = 500,

        [Description("Atualizado")]
        [Display(Name = "Atualizado")]
        Atualizado = 501,

        [Description("Importado")]
        [Display(Name = "Importado")]
        ImportacaoOk = 502,
        [Description("Falha na importação")]
        [Display(Name = "Falha na importação")]
        ImportacaoErro = -502,

        [Description("Convertido")]
        [Display(Name = "Convertido")]
        ConversaoOk = 503,
        [Description("Falha na conversão")]
        [Display(Name = "Falha na conversão")]
        ConversaoErro = -503,

        [Description("Ativo")]
        [Display(Name = "Ativo")]
        Ativo = 504,
        [Description("Inativo")]
        [Display(Name = "Inativo")]
        Inativo = -504,

        [Description("Desbloqueado")]
        [Display(Name = "Desbloqueado")]
        Desbloqueado = 505,
        [Description("Bloqueado")]
        [Display(Name = "Bloqueado")]
        Bloqueado = -505,

        [Description("Layout identificado")]
        [Display(Name = "Layout identificado")]
        LayoutIdentificacaoOk = 506,
        [Description("Falha na identificação do layout")]
        [Display(Name = "Falha na identificação do layout")]
        LayoutIdentificacaoErro = -506,

        [Description("Download efetuado")]
        [Display(Name = "Download efetuado")]
        DownloadOk = 507,
        [Description("Falha no download")]
        [Display(Name = "Falha no download")]
        DownloadErro = -507,

        [Description("Arquivo não localizado")]
        [Display(Name = "Arquivo não localizado")]
        ArquivoNaoLocalizado = -1,

        [Description("Arquivo sem dados")]
        [Display(Name = "Arquivo sem dados")]
        ArquivoSemDados = -2,
    }
}
