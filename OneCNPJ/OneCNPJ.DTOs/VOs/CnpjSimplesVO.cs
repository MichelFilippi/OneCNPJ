using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs
{
    public class CnpjSimplesVO
        : BaseVO,
          IEntityVO<CnpjSimples, CnpjSimplesVO, CnpjSimplesListaVO>
    {
        public string CnpjBasico { get; set; } = string.Empty;
        public virtual CnpjEmpresaVO? Empresa { get; set; }

        public bool OptanteSimples { get; set; }
        public DateTime? DataOpcaoSimples { get; set; }
        public DateTime? DataExclusaoSimples { get; set; }

        public bool OptanteMei { get; set; }
        public DateTime? DataOpcaoMei { get; set; }
        public DateTime? DataExclusaoMei { get; set; }

        public CnpjSimplesVO() { }

        public CnpjSimplesVO FromDomain(CnpjSimples model)
        {
            return new CnpjSimplesVO
            {
                Id = model.Id,
                CnpjBasico = model.CnpjBasico,
                OptanteSimples = model.OptanteSimples,
                DataOpcaoSimples = model.DataOpcaoSimples,
                DataExclusaoSimples = model.DataExclusaoSimples,
                OptanteMei = model.OptanteMei,
                DataOpcaoMei = model.DataOpcaoMei,
                DataExclusaoMei = model.DataExclusaoMei,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public CnpjSimplesListaVO ListFromDomain(CnpjSimples model)
        {
            return new CnpjSimplesListaVO
            {
                Id = model.Id,
                CnpjBasico = model.CnpjBasico,
                OptanteSimples = model.OptanteSimples,
                OptanteMei = model.OptanteMei,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public CnpjSimples ToDomain()
        {
            return new CnpjSimples
            {
                Id = this.Id,
                CnpjBasico = this.CnpjBasico,
                OptanteSimples = this.OptanteSimples,
                DataOpcaoSimples = this.DataOpcaoSimples,
                DataExclusaoSimples = this.DataExclusaoSimples,
                OptanteMei = this.OptanteMei,
                DataOpcaoMei = this.DataOpcaoMei,
                DataExclusaoMei = this.DataExclusaoMei,
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }
    }
}