using OneCNPJ.Common.Enums;
using OneCNPJ.Common.Utilities;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.DTOs.VOs.Presentation;

namespace OneCNPJ.DTOs.VOs
{
    public class CadfiVO 
        : BaseVO,
        IEntityVO<Cadfi, CadfiVO, CadfiListaVO>
    {
        public string Hash { get; set; }
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

        public CadfiVO() { Hash = General.GenerateSalt(30); }
        public CadfiVO(string hash) { Hash = hash; }

        public CadfiVO FromDomain(Cadfi model)
        {
            return new CadfiVO
            {
                Id = model.Id,
                Hash = model.Hash,
                StatusNaoAdaptados175 = model.StatusNaoAdaptados175,
                LinhasNaoAdaptados175 = model.LinhasNaoAdaptados175,
                LinhasImportadasNaoAdaptados175 = model.LinhasImportadasNaoAdaptados175,
                LinhasFalhasNaoAdaptados175 = model.LinhasFalhasNaoAdaptados175,
                LinhasIgnoradasNaoAdaptados175 = model.LinhasIgnoradasNaoAdaptados175,
                LinhasComErrosNaoAdaptados175 = model.LinhasComErrosNaoAdaptados175?.ToList() ?? [],
                StatusRegistroFundo = model.StatusRegistroFundo,
                LinhasRegistroFundo = model.LinhasRegistroFundo,
                LinhasImportadasRegistroFundo = model.LinhasImportadasRegistroFundo,
                LinhasFalhasRegistroFundo = model.LinhasFalhasRegistroFundo,
                LinhasIgnoradasRegistroFundo = model.LinhasIgnoradasRegistroFundo,
                LinhasComErrosRegistroFundo = model.LinhasComErrosRegistroFundo?.ToList() ?? [],
                StatusRegistroClasse = model.StatusRegistroClasse,
                LinhasRegistroClasse = model.LinhasRegistroClasse,
                LinhasImportadasRegistroClasse = model.LinhasImportadasRegistroClasse,
                LinhasFalhasRegistroClasse = model.LinhasFalhasRegistroClasse,
                LinhasIgnoradasRegistroClasse = model.LinhasIgnoradasRegistroClasse,
                LinhasComErrosRegistroClasse = model.LinhasComErrosRegistroClasse?.ToList() ?? [],
                StatusRegistroSubclasse = model.StatusRegistroSubclasse,
                LinhasRegistroSubclasse = model.LinhasRegistroSubclasse,
                LinhasImportadasRegistroSubclasse = model.LinhasImportadasRegistroSubclasse,
                LinhasFalhasRegistroSubclasse = model.LinhasFalhasRegistroSubclasse,
                LinhasIgnoradasRegistroSubclasse = model.LinhasIgnoradasRegistroSubclasse,
                LinhasComErrosRegistroSubclasse = model.LinhasComErrosRegistroSubclasse?.ToList() ?? [],
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public CadfiListaVO ListFromDomain(Cadfi model)
        {
            return new CadfiListaVO
            {
                Id = model.Id,
                Hash = model.Hash,
                StatusNaoAdaptados175 = model.StatusNaoAdaptados175,
                StatusRegistroFundo = model.StatusRegistroFundo,
                StatusRegistroClasse = model.StatusRegistroClasse,
                StatusRegistroSubclasse = model.StatusRegistroSubclasse,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public Cadfi ToDomain()
        {
            return new Cadfi
            {
                Id = this.Id,
                Hash = this.Hash,
                StatusNaoAdaptados175 = this.StatusNaoAdaptados175,
                LinhasNaoAdaptados175 = this.LinhasNaoAdaptados175,
                LinhasImportadasNaoAdaptados175 = this.LinhasImportadasNaoAdaptados175,
                LinhasFalhasNaoAdaptados175 = this.LinhasFalhasNaoAdaptados175,
                LinhasIgnoradasNaoAdaptados175 = this.LinhasIgnoradasNaoAdaptados175,
                LinhasComErrosNaoAdaptados175 = this.LinhasComErrosNaoAdaptados175?.ToList() ?? [],
                StatusRegistroFundo = this.StatusRegistroFundo,
                LinhasRegistroFundo = this.LinhasRegistroFundo,
                LinhasImportadasRegistroFundo = this.LinhasImportadasRegistroFundo,
                LinhasFalhasRegistroFundo = this.LinhasFalhasRegistroFundo,
                LinhasIgnoradasRegistroFundo = this.LinhasIgnoradasRegistroFundo,
                LinhasComErrosRegistroFundo = this.LinhasComErrosRegistroFundo?.ToList() ?? [],
                StatusRegistroClasse = this.StatusRegistroClasse,
                LinhasRegistroClasse = this.LinhasRegistroClasse,
                LinhasImportadasRegistroClasse = this.LinhasImportadasRegistroClasse,
                LinhasFalhasRegistroClasse = this.LinhasFalhasRegistroClasse,
                LinhasIgnoradasRegistroClasse = this.LinhasIgnoradasRegistroClasse,
                LinhasComErrosRegistroClasse = this.LinhasComErrosRegistroClasse?.ToList() ?? [],
                StatusRegistroSubclasse = this.StatusRegistroSubclasse,
                LinhasRegistroSubclasse = this.LinhasRegistroSubclasse,
                LinhasImportadasRegistroSubclasse = this.LinhasImportadasRegistroSubclasse,
                LinhasFalhasRegistroSubclasse = this.LinhasFalhasRegistroSubclasse,
                LinhasIgnoradasRegistroSubclasse = this.LinhasIgnoradasRegistroSubclasse,
                LinhasComErrosRegistroSubclasse = this.LinhasComErrosRegistroSubclasse?.ToList() ?? [],
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }

        public CadfiStatusVO ToStatus()
        {
            return new CadfiStatusVO
            {
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao,
                Hash = this.Hash,
                StatusNaoAdaptados175 = this.StatusNaoAdaptados175,
                StatusRegistroFundo = this.StatusRegistroFundo,
                StatusRegistroClasse = this.StatusRegistroClasse,
                StatusRegistroSubclasse = this.StatusRegistroSubclasse
            };
        }
    }
}
