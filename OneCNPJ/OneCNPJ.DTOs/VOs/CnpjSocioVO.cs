using OneCNPJ.Domain;
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
    public class CnpjSocioVO
        : BaseVO,
          IEntityVO<CnpjSocio, CnpjSocioVO, CnpjSocioListaVO>
    {
        public long CnpjBasico { get; set; } 
        public virtual CnpjEmpresaVO? Empresa { get; set; }

        public string TipoSocio { get; set; } = string.Empty;
        public string NomeSocio { get; set; } = string.Empty;
        public string DocumentoSocio { get; set; } = string.Empty;

        public long QualificacaoSocioId { get; set; }
        public virtual QualificacaoSocioVO? QualificacaoSocio { get; set; }

        public DateTime? DataEntradaSociedade { get; set; }

        public CnpjSocioVO() { }

        public CnpjSocioVO FromDomain(CnpjSocio model)
        {
            return new CnpjSocioVO
            {
                Id = model.Id,
                CnpjBasico = model.CnpjBasico,
                TipoSocio = model.TipoSocio,
                NomeSocio = model.NomeSocio,
                DocumentoSocio = model.DocumentoSocio,
                QualificacaoSocioId = model.QualificacaoSocioId,
                DataEntradaSociedade = model.DataEntradaSociedade,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public CnpjSocioListaVO ListFromDomain(CnpjSocio model)
        {
            return new CnpjSocioListaVO
            {
                Id = model.Id,
                CnpjBasico = model.CnpjBasico,
                TipoSocio = model.TipoSocio,
                NomeSocio = model.NomeSocio,
                DocumentoSocio = model.DocumentoSocio,
                QualificacaoSocioId = model.QualificacaoSocioId,
                DataEntradaSociedade = model.DataEntradaSociedade,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public CnpjSocio ToDomain()
        {
            return new CnpjSocio
            {
                Id = this.Id,
                CnpjBasico = this.CnpjBasico,
                TipoSocio = this.TipoSocio,
                NomeSocio = this.NomeSocio,
                DocumentoSocio = this.DocumentoSocio,
                QualificacaoSocioId = this.QualificacaoSocioId,
                DataEntradaSociedade = this.DataEntradaSociedade,
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }
    }

}
