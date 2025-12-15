using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Satelites.Listas;
using System.Diagnostics.CodeAnalysis;

namespace OneCNPJ.DTOs.VOs.Satelites
{
    public class LayoutCampoVO
        : BaseVO, 
        IEntityVO<LayoutCampo, LayoutCampoVO, LayoutCampoListaVO>
    {
        public string Cabecalho { get; set; } = string.Empty;
        public string CabecalhoOrdem { get; set; } = string.Empty;
        public bool Obrigatorio { get; set; } = false;
        public string AtributoClasse { get; set; } = string.Empty;
        public string AtributoClasseAmigavel { get; set; } = string.Empty;
        public bool ModelObrigatorio { get; set; } = false;
        public int Ordem { get; set; } = int.MaxValue;
        public long LayoutId { get; set; }
        public Layout Layout { get; set; } = null!;

        public LayoutCampoVO() { }

        public LayoutCampoVO FromDomain(LayoutCampo entity)
        {
            return new LayoutCampoVO
            {
                Id = entity.Id,
                Status = entity.Status,
                DataCriacao = entity.DataCriacao,
                DataAtualizacao = entity.DataAtualizacao,
                Cabecalho = entity.Cabecalho,
                CabecalhoOrdem = entity.CabecalhoOrdem,
                Obrigatorio = entity.Obrigatorio,
                AtributoClasse = entity.AtributoClasse,
                AtributoClasseAmigavel = entity.AtributoClasseAmigavel,
                ModelObrigatorio = entity.ModelObrigatorio,
                Ordem = entity.Ordem,
                LayoutId = entity.LayoutId,
            };
        }

        public LayoutCampoListaVO ListFromDomain(LayoutCampo entity)
        {
            return new LayoutCampoListaVO
            {
                Id = entity.Id,
                Status = entity.Status,
                DataCriacao = entity.DataCriacao,
                DataAtualizacao = entity.DataAtualizacao,
                Cabecalho = entity.Cabecalho,
                CabecalhoOrdem = entity.CabecalhoOrdem,
                Obrigatorio = entity.Obrigatorio,
                AtributoClasse = entity.AtributoClasse,
                AtributoClasseAmigavel = entity.AtributoClasseAmigavel,
                ModelObrigatorio = entity.ModelObrigatorio,
                Ordem = entity.Ordem,
                LayoutId = entity.LayoutId,
            };
        }

        public LayoutCampo ToDomain()
        {
            return new LayoutCampo
            {
                Id = this.Id,
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao,
                Cabecalho = this.Cabecalho,
                CabecalhoOrdem = this.CabecalhoOrdem,
                Obrigatorio = this.Obrigatorio,
                AtributoClasse = this.AtributoClasse,
                AtributoClasseAmigavel = this.AtributoClasseAmigavel,
                ModelObrigatorio = this.ModelObrigatorio,
                Ordem = this.Ordem,
                LayoutId = this.LayoutId,
            };
        }

        public class ComparadorCabecalhoECampo : IEqualityComparer<LayoutCampoVO>
        {
            public bool Equals(LayoutCampoVO? x, LayoutCampoVO? y)
            {
                if (x == null || y == null)
                    return false;

                return x.Cabecalho == y.Cabecalho && x.CabecalhoOrdem == y.CabecalhoOrdem;
            }

            public int GetHashCode([DisallowNull] LayoutCampoVO obj)
            {
                if (obj == null)
                    return 0;

                int hashHeaderColumn = obj.Cabecalho == null ? 0 : obj.Cabecalho.GetHashCode();
                int hashOrderColumn = obj.CabecalhoOrdem == null ? 0 : obj.CabecalhoOrdem.GetHashCode();

                return hashHeaderColumn ^ hashOrderColumn;
            }
        }
    }
}
