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
    public class CnpjEstabelecimentoCnaeSecundarioVO
        : BaseVO,
          IEntityVO<
              CnpjEstabelecimentoCnaeSecundario,
              CnpjEstabelecimentoCnaeSecundarioVO,
              CnpjEstabelecimentoCnaeSecundarioListaVO>
    {
        public long ImportacaoId { get; set; }
        public long EstabelecimentoId { get; set; }

        public long CnaeId { get; set; }
        public virtual CnaeVO? Cnae { get; set; }

        public CnpjEstabelecimentoCnaeSecundarioVO() { }

        public CnpjEstabelecimentoCnaeSecundarioVO FromDomain(CnpjEstabelecimentoCnaeSecundario model)
        {
            return new CnpjEstabelecimentoCnaeSecundarioVO
            {
                Id = model.Id,
                ImportacaoId = model.ImportacaoId,
                EstabelecimentoId = model.EstabelecimentoId,
                CnaeId = model.CnaeId,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public CnpjEstabelecimentoCnaeSecundarioListaVO ListFromDomain(CnpjEstabelecimentoCnaeSecundario model)
        {
            return new CnpjEstabelecimentoCnaeSecundarioListaVO
            {
                Id = model.Id,
                ImportacaoId = model.ImportacaoId,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public CnpjEstabelecimentoCnaeSecundario ToDomain()
        {
            return new CnpjEstabelecimentoCnaeSecundario
            {
                Id = this.Id,
                ImportacaoId = this.ImportacaoId,
                EstabelecimentoId = this.EstabelecimentoId,
                CnaeId = this.CnaeId,
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }
    }
}