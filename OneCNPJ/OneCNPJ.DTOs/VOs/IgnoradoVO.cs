using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs
{
    public class IgnoradoVO
         : BaseVO,
         IEntityVO<Ignorado, IgnoradoVO, IgnoradoListaVO>
    {
        public long CnpjEmpresaId { get; set; }
        public virtual CnpjEmpresaVO? CnpjEmpresa { get; set; }
        public long Linha { get; set; }
        public List<string> Cabecalho { get; set; } = [];
        public List<string> Conteudo { get; set; } = [];
        public List<string> Motivo { get; set; } = [];

        public IgnoradoVO() { }

        public IgnoradoVO FromDomain(Ignorado model)
        {
            return new IgnoradoVO
            {
                Id = model.Id,
                CnpjEmpresaId = model.CnpjEmpresaId,
                Linha = model.Linha,
                Cabecalho = model.Cabecalho,
                Conteudo = model.Conteudo,
                Motivo = model.Motivo,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public IgnoradoListaVO ListFromDomain(Ignorado model)
        {
            return new IgnoradoListaVO
            {
                Id = model.Id,
                CnpjEmpresaId = model.CnpjEmpresaId,
                Linha = model.Linha,
                Cabecalho = model.Cabecalho,
                Conteudo = model.Conteudo,
                Motivo = model.Motivo,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public Ignorado ToDomain()
        {
            return new Ignorado
            {
                Id = this.Id,
                CnpjEmpresaId = this.CnpjEmpresaId,
                Linha = this.Linha,
                Cabecalho = this.Cabecalho,
                Conteudo = this.Conteudo,
                Motivo = this.Motivo,
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }
    }
}
