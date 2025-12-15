using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs.Satelites
{
    public class MunicipioVO
      : BaseVO,
        IEntityVO<Municipio, MunicipioVO, MunicipioListaVO>
    {
        public string Descricao { get; set; } = string.Empty;

        public MunicipioVO() { }

        public MunicipioVO FromDomain(Municipio model)
        {
            return new MunicipioVO
            {

                Descricao = model.Descricao
            };
        }

        public MunicipioListaVO ListFromDomain(Municipio model)
        {
            return new MunicipioListaVO
            {
                Descricao = model.Descricao
            };
        }

        public Municipio ToDomain()
        {
            return new Municipio
            {
                Descricao = this.Descricao
            };
        }
    }

}