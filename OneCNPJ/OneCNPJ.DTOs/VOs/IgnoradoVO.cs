using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.DTOs.VOs
{
    public class IgnoradoVO
        : BaseVO,
        IEntityVO<Ignorado, IgnoradoVO, IgnoradoListaVO>
    {
        public long CadfiId { get; set; }
        public virtual CadfiVO? Cadfi { get; set; }
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
                CadfiId = model.CadfiId,
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
                CadfiId = model.CadfiId,
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
                CadfiId = this.CadfiId,
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
