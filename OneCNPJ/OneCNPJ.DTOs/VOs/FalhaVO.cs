using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs
{
    public class FalhaVO
     : BaseVO,
     IEntityVO<Falha, FalhaVO, FalhaListaVO>
    {
        public long CnpjEmpresaId { get; set; }
        public virtual CnpjEmpresaVO? CnpjEmpresa { get; set; }
        public int Linha { get; set; }
        public List<string> LinhaConteudo { get; set; } = [];
        public List<string> ColunaOrigem { get; set; } = [];
        public List<string> CampoDestino { get; set; } = [];
        public List<string> ValorRecebido { get; set; } = [];
        public List<string> Motivo { get; set; } = [];

        public FalhaVO() { }

        public FalhaVO FromDomain(Falha model)
        {
            return new FalhaVO
            {
                Id = model.Id,
                CnpjEmpresaId = model.CnpjEmpresaId,
                Linha = model.Linha,
                LinhaConteudo = model.LinhaConteudo,
                ColunaOrigem = model.ColunaOrigem,
                CampoDestino = model.CampoDestino,
                ValorRecebido = model.ValorRecebido,
                Motivo = model.Motivo,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public FalhaListaVO ListFromDomain(Falha model)
        {
            return new FalhaListaVO
            {
                Id = model.Id,
                CnpjEmpresaId = model.CnpjEmpresaId,
                Linha = model.Linha,
                LinhaConteudo = model.LinhaConteudo,
                ColunaOrigem = model.ColunaOrigem,
                CampoDestino = model.CampoDestino,
                ValorRecebido = model.ValorRecebido,
                Motivo = model.Motivo,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public Falha ToDomain()
        {
            return new Falha
            {
                Id = this.Id,
                CnpjEmpresaId = this.CnpjEmpresaId,
                Linha = this.Linha,
                LinhaConteudo = this.LinhaConteudo,
                ColunaOrigem = this.ColunaOrigem,
                CampoDestino = this.CampoDestino,
                ValorRecebido = this.ValorRecebido,
                Motivo = this.Motivo,
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }
    }
}
