using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs.Satelites
{
    public class QualificacaoSocioVO
        : BaseVO,
          IEntityVO<QualificacaoSocio, QualificacaoSocioVO, QualificacaoSocioListaVO>
    {
        public string Descricao { get; set; } = string.Empty;

        public QualificacaoSocioVO() { }

        public QualificacaoSocioVO FromDomain(QualificacaoSocio model)
        {
            return new QualificacaoSocioVO
            {
                Descricao = model.Descricao
            };
        }

        public QualificacaoSocioListaVO ListFromDomain(QualificacaoSocio model)
        {
            return new QualificacaoSocioListaVO
            {
                Descricao = model.Descricao
            };
        }

        public QualificacaoSocio ToDomain()
        {
            return new QualificacaoSocio
            {
                Descricao = this.Descricao
            };
        }
    }
}