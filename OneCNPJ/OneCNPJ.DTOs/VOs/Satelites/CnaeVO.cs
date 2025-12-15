using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs.Satelites
{
    public class CnaeVO
           : BaseVO,
             IEntityVO<Cnae, CnaeVO, CnaeListaVO>
    {
        public string Descricao { get; set; } = string.Empty;

        public CnaeVO() { }

        public CnaeVO FromDomain(Cnae model)
        {
            return new CnaeVO
            {
                Descricao = model.Descricao
            };
        }

        public CnaeListaVO ListFromDomain(Cnae model)
        {
            return new CnaeListaVO
            {
                Descricao = model.Descricao
            };
        }

        public Cnae ToDomain()
        {
            return new Cnae
            {
                Descricao = this.Descricao
            };
        }
    }
}