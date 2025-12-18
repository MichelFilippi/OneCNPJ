using OneCNPJ.Common.Enums;
using OneCNPJ.Domain;
using OneCNPJ.Domain.Models;
using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.DTOs.VOs.Presentation;
using OneCNPJ.DTOs.VOs.Satelites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs
{
    public class CnpjEmpresaVO
        : BaseVO,
          IEntityVO<CnpjEmpresa, CnpjEmpresaVO, CnpjEmpresaListaVO>
    {
        public string CnpjBasico { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;

        public long NaturezaJuridicaId { get; set; } 

        public long QualificacaoResponsavelId { get; set; } 

        public string CapitalSocial { get; set; } = string.Empty;

        public PorteEmpresaEnum Porte { get; set; } = PorteEmpresaEnum.NaoInformado;

        public string? EnteFederativoResponsavel { get; set; }

        public virtual List<CnpjSocioVO> Socios { get; set; } = [];
        public virtual CnpjSimplesVO? Simples { get; set; }
        public virtual List<CnpjEstabelecimentoVO> Estabelecimentos { get; set; } = [];

        public StatusEnum StatusNaoAdaptados175 { get; set; } = StatusEnum.Processamento;
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

        public CnpjEmpresaVO() { }

        public CnpjEmpresaVO FromDomain(CnpjEmpresa model)
        {
            return new CnpjEmpresaVO
            {
                Id = model.Id,
                CnpjBasico = model.CnpjBasico,
                RazaoSocial = model.RazaoSocial,
                NaturezaJuridicaId = model.NaturezaJuridicaId,
                QualificacaoResponsavelId = model.QualificacaoResponsavelId,
                CapitalSocial = model.CapitalSocial,
                Porte = model.Porte,
                EnteFederativoResponsavel = model.EnteFederativoResponsavel,
                StatusNaoAdaptados175 = model.StatusNaoAdaptados175,
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

        public CnpjEmpresaListaVO ListFromDomain(CnpjEmpresa model)
        {
            return new CnpjEmpresaListaVO
            {
                Id = model.Id,
                CnpjBasico = model.CnpjBasico,
                RazaoSocial = model.RazaoSocial,
                NaturezaJuridicaId = model.NaturezaJuridicaId,
                QualificacaoResponsavelId = model.QualificacaoResponsavelId,
                CapitalSocial = model.CapitalSocial,
                Porte = model.Porte,
                EnteFederativoResponsavel = model.EnteFederativoResponsavel,
                StatusNaoAdaptados175 = model.StatusNaoAdaptados175,
                StatusRegistroFundo = model.StatusRegistroFundo,
                StatusRegistroClasse = model.StatusRegistroClasse,
                StatusRegistroSubclasse = model.StatusRegistroSubclasse,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public CnpjEmpresa ToDomain()
        {
            return new CnpjEmpresa
            {
                Id = this.Id,
                CnpjBasico = this.CnpjBasico,
                RazaoSocial = this.RazaoSocial,
                NaturezaJuridicaId = this.NaturezaJuridicaId,
                QualificacaoResponsavelId = this.QualificacaoResponsavelId,
                CapitalSocial = this.CapitalSocial,
                Porte = this.Porte,
                EnteFederativoResponsavel = this.EnteFederativoResponsavel,
                StatusNaoAdaptados175 = this.StatusNaoAdaptados175,
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
        public CnpjEmpresaStatusVO ToStatus()
        {
            return new CnpjEmpresaStatusVO
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
