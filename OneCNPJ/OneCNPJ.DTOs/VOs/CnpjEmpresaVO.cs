using OneCNPJ.Common.Enums;
using OneCNPJ.Domain;
using OneCNPJ.Domain.Models;
using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Listas;
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

        public string NaturezaJuridicaId { get; set; } = string.Empty;
        public virtual NaturezaJuridicaVO? NaturezaJuridica { get; set; }

        public string QualificacaoResponsavelId { get; set; } = string.Empty;
        public virtual QualificacaoSocioVO? QualificacaoResponsavel { get; set; }

        public string CapitalSocial { get; set; } = string.Empty;

        public PorteEmpresaEnum Porte { get; set; } = PorteEmpresaEnum.NaoInformado;

        public string? EnteFederativoResponsavel { get; set; }

        public virtual List<CnpjSocioVO> Socios { get; set; } = [];
        public virtual CnpjSimplesVO? Simples { get; set; }
        public virtual List<CnpjEstabelecimentoVO> Estabelecimentos { get; set; } = [];

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
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }
    }
}
