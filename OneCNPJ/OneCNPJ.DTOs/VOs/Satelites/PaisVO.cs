using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs.Satelites
{
    public class PaisVO
        : BaseVO,
          IEntityVO<Pais, PaisVO, PaisListaVO>
    {
        public string Descricao { get; set; } = string.Empty;

        public PaisVO() { }

        public PaisVO FromDomain(Pais model)
        {
            return new PaisVO
            {
                Descricao = model.Descricao
            };
        }

        public PaisListaVO ListFromDomain(Pais model)
        {
            return new PaisListaVO
            {
                Descricao = model.Descricao
            };
        }

        public Pais ToDomain()
        {
            return new Pais
            {
                Descricao = this.Descricao
            };
        }
    }
}