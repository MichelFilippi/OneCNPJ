using OneCNPJ.Common.Enums;
using OneCNPJ.Domain;
using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;

namespace OneCNPJ.DTOs.VOs.Satelites
{
    public class LayoutVO
        : BaseVO,
        IEntityVO<Layout, LayoutVO, LayoutListaVO>
    {
        public string Descricao { get; set; } = string.Empty;
        public int LinhaCabecalho { get; set; }
        public int LinhaDados { get; set; }
        public FormatoCadfiEnum FormatoCadfiEnum { get; set; } = FormatoCadfiEnum.NaoDefinido;

        public List<LayoutCampoVO> Campos { get; set; } = [];

        public LayoutVO() { }

        public LayoutVO FromDomain(Layout entity)
        {
            return new LayoutVO
            {
                Id = entity.Id,
                Status = entity.Status,
                DataCriacao = entity.DataCriacao,
                DataAtualizacao = entity.DataAtualizacao,
                Descricao = entity.Descricao,
                LinhaCabecalho = entity.LinhaCabecalho,
                LinhaDados = entity.LinhaDados,
                FormatoCadfiEnum = entity.FormatoCadfiEnum,
                Campos = entity.Campos?
                    .Select(c => new LayoutCampoVO().FromDomain(c))
                    .ToList() ?? []
            };
        }

        public LayoutListaVO ListFromDomain(Layout entity)
        {
            return new LayoutListaVO
            {
                Id = entity.Id,
                Status = entity.Status,
                DataCriacao = entity.DataCriacao,
                DataAtualizacao = entity.DataAtualizacao,
                Descricao = entity.Descricao,
                LinhaCabecalho = entity.LinhaCabecalho,
                LinhaDados = entity.LinhaDados,
                FormatoCadfiEnum = entity.FormatoCadfiEnum,
                Campos = entity.Campos?
                    .Select(c => new LayoutCampoVO().FromDomain(c))
                    .ToList() ?? []
            };
        }

        public Layout ToDomain()
        {
            return new Layout 
            { 
                Id = this.Id,
                Status = this.Status,
                DataCriacao= this.DataCriacao,
                DataAtualizacao= this.DataAtualizacao,
                Descricao = this.Descricao,
                LinhaCabecalho = this.LinhaCabecalho,
                LinhaDados = this.LinhaDados,
                FormatoCadfiEnum = this.FormatoCadfiEnum,
                Campos = this.Campos?
                    .Select(c => new LayoutCampoVO().ToDomain())
                    .ToList() ?? []
            };
        }
    }
}
