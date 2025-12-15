using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs.Satelites
{
    public class MotivoSituacaoCadastralVO
        : BaseVO,
          IEntityVO<MotivoSituacaoCadastral, MotivoSituacaoCadastralVO, MotivoSituacaoCadastralListaVO>
    {
        public string Descricao { get; set; } = string.Empty;

        public MotivoSituacaoCadastralVO() { }

        public MotivoSituacaoCadastralVO FromDomain(MotivoSituacaoCadastral model)
        {
            return new MotivoSituacaoCadastralVO
            {
                Descricao = model.Descricao
            };
        }

        public MotivoSituacaoCadastralListaVO ListFromDomain(MotivoSituacaoCadastral model)
        {
            return new MotivoSituacaoCadastralListaVO
            {
                Descricao = model.Descricao
            };
        }

        public MotivoSituacaoCadastral ToDomain()
        {
            return new MotivoSituacaoCadastral
            {
                Descricao = this.Descricao
            };
        }
    }
}
