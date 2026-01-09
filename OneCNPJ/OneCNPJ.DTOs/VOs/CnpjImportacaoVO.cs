using OneCNPJ.Common.Enums;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.DTOs.VOs.Presentation;

namespace OneCNPJ.DTOs.VOs
{
    public class CnpjImportacaoVO
        : BaseVO,
          IEntityVO<CnpjImportacao, CnpjImportacaoVO, CnpjImportacaoListaVO>
    {
        public DateTime DataReferencia { get; set; }
        public string VersaoOrigem { get; set; } = string.Empty;
        public string UrlOrigem { get; set; } = string.Empty;

        public StatusEnum Status { get; set; }
        public DateTime InicioEm { get; set; }
        public DateTime? FimEm { get; set; }
        public string? Mensagem { get; set; }

        public StatusEnum StatusEmpresas { get; set; }
        public long LinhasEmpresasTotal { get; set; }
        public long LinhasEmpresasImportadas { get; set; }
        public long LinhasEmpresasFalhas { get; set; }

        public StatusEnum StatusEstabelecimentos { get; set; }
        public long LinhasEstabelecimentosTotal { get; set; }
        public long LinhasEstabelecimentosImportadas { get; set; }
        public long LinhasEstabelecimentosFalhas { get; set; }

        public StatusEnum StatusSocios { get; set; }
        public long LinhasSociosTotal { get; set; }
        public long LinhasSociosImportadas { get; set; }
        public long LinhasSociosFalhas { get; set; }

        public StatusEnum StatusSimples { get; set; }
        public long LinhasSimplesTotal { get; set; }
        public long LinhasSimplesImportadas { get; set; }
        public long LinhasSimplesFalhas { get; set; }

        public StatusEnum StatusSatelites { get; set; }
        public long LinhasSatelitesImportadas { get; set; }
        public long LinhasSatelitesFalhas { get; set; }
        public StatusEnum StatusNaoAdaptados175 { get; set; } = StatusEnum.Processamento;
        public int LinhasNaoAdaptados175 { get; set; } = 0;
        public int LinhasImportadasNaoAdaptados175 { get; set; } = 0;
        public int LinhasFalhasNaoAdaptados175 { get; set; } = 0;
        public int LinhasIgnoradasNaoAdaptados175 { get; set; } = 0;
        public List<int> LinhasComErrosNaoAdaptados175 { get; set; } = [];
        public StatusEnum StatusRegistroFundo { get; set; } = StatusEnum.Processamento;
        public int LinhasRegistroFundo { get; set; } = 0;
        public int LinhasImportadasRegistroFundo { get; set; } = 0;
        public int LinhasFalhasRegistroFundo { get; set; } = 0;
        public int LinhasIgnoradasRegistroFundo { get; set; } = 0;
        public List<int> LinhasComErrosRegistroFundo { get; set; } = [];
        public StatusEnum StatusRegistroClasse { get; set; } = StatusEnum.Processamento;
        public int LinhasRegistroClasse { get; set; } = 0;
        public int LinhasImportadasRegistroClasse { get; set; } = 0;
        public int LinhasFalhasRegistroClasse { get; set; } = 0;
        public int LinhasIgnoradasRegistroClasse { get; set; } = 0;
        public List<int> LinhasComErrosRegistroClasse { get; set; } = [];
        public StatusEnum StatusRegistroSubclasse { get; set; } = StatusEnum.Processamento;
        public int LinhasRegistroSubclasse { get; set; } = 0;
        public int LinhasImportadasRegistroSubclasse { get; set; } = 0;
        public int LinhasFalhasRegistroSubclasse { get; set; } = 0;
        public int LinhasIgnoradasRegistroSubclasse { get; set; } = 0;
        public List<int> LinhasComErrosRegistroSubclasse { get; set; } = [];
        public virtual List<CnpjEmpresaVO> CnpjEmpresa { get; set; } = [];

        public CnpjImportacaoVO() { }

        public CnpjImportacaoVO FromDomain(CnpjImportacao model)
        {
            return new CnpjImportacaoVO
            {
                Id = model.Id,
                DataReferencia = model.DataReferencia,
                VersaoOrigem = model.VersaoOrigem,
                UrlOrigem = model.UrlOrigem,
                Status = model.Status,
                InicioEm = model.InicioEm,
                FimEm = model.FimEm,
                Mensagem = model.Mensagem,

                StatusEmpresas = model.StatusEmpresas,
                LinhasEmpresasTotal = model.LinhasEmpresasTotal,
                LinhasEmpresasImportadas = model.LinhasEmpresasImportadas,
                LinhasEmpresasFalhas = model.LinhasEmpresasFalhas,

                StatusEstabelecimentos = model.StatusEstabelecimentos,
                LinhasEstabelecimentosTotal = model.LinhasEstabelecimentosTotal,
                LinhasEstabelecimentosImportadas = model.LinhasEstabelecimentosImportadas,
                LinhasEstabelecimentosFalhas = model.LinhasEstabelecimentosFalhas,

                StatusSocios = model.StatusSocios,
                LinhasSociosTotal = model.LinhasSociosTotal,
                LinhasSociosImportadas = model.LinhasSociosImportadas,
                LinhasSociosFalhas = model.LinhasSociosFalhas,

                StatusSimples = model.StatusSimples,
                LinhasSimplesTotal = model.LinhasSimplesTotal,
                LinhasSimplesImportadas = model.LinhasSimplesImportadas,
                LinhasSimplesFalhas = model.LinhasSimplesFalhas,

                StatusSatelites = model.StatusSatelites,
                LinhasSatelitesImportadas = model.LinhasSatelitesImportadas,
                LinhasSatelitesFalhas = model.LinhasSatelitesFalhas,

                CnpjEmpresa = model.CnpjEmpresa?
                    .Select(e => new CnpjEmpresaVO().FromDomain(e))
                    .ToList() ?? [],

                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }


        public CnpjImportacaoListaVO ListFromDomain(CnpjImportacao model)
        {
            return new CnpjImportacaoListaVO
            {
                Id = model.Id,
                DataReferencia = model.DataReferencia,
                VersaoOrigem = model.VersaoOrigem,
                Status = model.Status,
                InicioEm = model.InicioEm,
                FimEm = model.FimEm,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public CnpjImportacao ToDomain()
        {
            return new CnpjImportacao
            {
                Id = this.Id,
                DataReferencia = this.DataReferencia,
                VersaoOrigem = this.VersaoOrigem,
                UrlOrigem = this.UrlOrigem,
                Status = this.Status,
                InicioEm = this.InicioEm,
                FimEm = this.FimEm,
                Mensagem = this.Mensagem,

                StatusEmpresas = this.StatusEmpresas,
                LinhasEmpresasTotal = this.LinhasEmpresasTotal,
                LinhasEmpresasImportadas = this.LinhasEmpresasImportadas,
                LinhasEmpresasFalhas = this.LinhasEmpresasFalhas,

                StatusEstabelecimentos = this.StatusEstabelecimentos,
                LinhasEstabelecimentosTotal = this.LinhasEstabelecimentosTotal,
                LinhasEstabelecimentosImportadas = this.LinhasEstabelecimentosImportadas,
                LinhasEstabelecimentosFalhas = this.LinhasEstabelecimentosFalhas,

                StatusSocios = this.StatusSocios,
                LinhasSociosTotal = this.LinhasSociosTotal,
                LinhasSociosImportadas = this.LinhasSociosImportadas,
                LinhasSociosFalhas = this.LinhasSociosFalhas,

                StatusSimples = this.StatusSimples,
                LinhasSimplesTotal = this.LinhasSimplesTotal,
                LinhasSimplesImportadas = this.LinhasSimplesImportadas,
                LinhasSimplesFalhas = this.LinhasSimplesFalhas,

                StatusSatelites = this.StatusSatelites,
                LinhasSatelitesImportadas = this.LinhasSatelitesImportadas,
                LinhasSatelitesFalhas = this.LinhasSatelitesFalhas,

                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }
        public CnpjImportacaoStatusVO ToStatus()
        {
            return new CnpjImportacaoStatusVO
            {
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao,
                StatusNaoAdaptados175 = this.StatusNaoAdaptados175,
                StatusRegistroFundo = this.StatusRegistroFundo,
                StatusRegistroClasse = this.StatusRegistroClasse,
                StatusRegistroSubclasse = this.StatusRegistroSubclasse
            };
        }
    }
}
