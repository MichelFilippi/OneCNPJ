using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public long ImportacaoId { get; set; }
        public string CnpjBasico { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;

        public long NaturezaJuridicaId { get; set; } 
        public virtual NaturezaJuridicaVO NaturezaJuridica { get; set; }

        public long QualificacaoSocioId { get; set; }
        public virtual QualificacaoSocioVO QualificacaoSocio { get; set; }

        public string CapitalSocial { get; set; } = string.Empty;

        public PorteEmpresaEnum Porte { get; set; } = PorteEmpresaEnum.NaoInformado;

        public string? EnteFederativoResponsavel { get; set; }

        public virtual List<CnpjSocioVO> Socios { get; set; } = [];
        public virtual List<CnpjEstabelecimentoVO>  Estabelecimentos { get; set; } = [];
        public virtual CnpjSimplesVO? Simples { get; set; }

        public CnpjEmpresaVO() { }

        public CnpjEmpresaVO FromDomain(CnpjEmpresa model)
        {
            return new CnpjEmpresaVO
            {
                Id = model.Id,
                ImportacaoId = model.ImportacaoId,
                CnpjBasico = model.CnpjBasico,
                RazaoSocial = model.RazaoSocial,
                NaturezaJuridicaId = model.NaturezaJuridicaId,
                QualificacaoSocioId = model.QualificacaoSocioId,
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
                ImportacaoId = model.ImportacaoId,
                CnpjBasico = model.CnpjBasico,
                RazaoSocial = model.RazaoSocial,
                NaturezaJuridicaId = model.NaturezaJuridicaId,
                QualificacaoSocioId = model.QualificacaoSocioId,
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
                ImportacaoId = this.ImportacaoId,
                CnpjBasico = this.CnpjBasico,
                RazaoSocial = this.RazaoSocial,
                NaturezaJuridicaId = this.NaturezaJuridicaId,
                QualificacaoSocioId = this.QualificacaoSocioId,
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
