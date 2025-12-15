using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs.Satelites
{
    public class NaturezaJuridicaVO
        : BaseVO,
          IEntityVO<NaturezaJuridica, NaturezaJuridicaVO, NaturezaJuridicaListaVO>
    {
        public string Descricao { get; set; } = string.Empty;

        public NaturezaJuridicaVO() { }

        public NaturezaJuridicaVO FromDomain(NaturezaJuridica model)
        {
            return new NaturezaJuridicaVO
            {
                Descricao = model.Descricao
            };
        }

        public NaturezaJuridicaListaVO ListFromDomain(NaturezaJuridica model)
        {
            return new NaturezaJuridicaListaVO
            {
                Descricao = model.Descricao
            };
        }

        public NaturezaJuridica ToDomain()
        {
            return new NaturezaJuridica
            {
                Descricao = this.Descricao
            };
        }
    }
}