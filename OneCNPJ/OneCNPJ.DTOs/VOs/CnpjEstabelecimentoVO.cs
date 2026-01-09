using OneCNPJ.Domain.Models;
using OneCNPJ.Domain.Models.Satelites;
using OneCNPJ.DTOs.VOs.Listas;
using OneCNPJ.DTOs.VOs.Satelites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.DTOs.VOs
{
    public class CnpjEstabelecimentoVO
        : BaseVO,
          IEntityVO<CnpjEstabelecimento, CnpjEstabelecimentoVO, CnpjEstabelecimentoListaVO>
    {
        public long CnpjBasico { get; set; }

        public long ImportacaoId { get; set; }
        public long CnpjEmpresaId { get; set; }

        public string CnpjOrdem { get; set; } = string.Empty;
        public string CnpjDv { get; set; } = string.Empty;

        public string IdentificadorMatrizFilial { get; set; } = string.Empty;
        public string? NomeFantasia { get; set; }

        public string SituacaoCadastral { get; set; } = string.Empty;
        public DateTime? DataSituacaoCadastral { get; set; }

        public long MotivoSituacaoCadastralId { get; set; } 
        public virtual MotivoSituacaoCadastralVO MotivoSituacaoCadastral { get; set; } 

        public string? NomeCidadeExterior { get; set; }


        public DateTime? DataInicioAtividade { get; set; }

        public long CnaeId { get; set; } 
        public virtual CnaeVO Cnae { get; set; }

        public virtual List<CnpjEstabelecimentoCnaeSecundarioVO> CnaesSecundarios { get; set; } = [];

        public string? TipoLogradouro { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cep { get; set; }
        public string? Uf { get; set; }
        public long? PaisId { get; set; }
        public virtual PaisVO? Pais { get; set; }
        public long MunicipioId { get; set; } 
        public virtual MunicipioVO? Municipio { get; set; }

        public CnpjEstabelecimentoVO() { }

        public CnpjEstabelecimentoVO FromDomain(CnpjEstabelecimento model)
        {
            return new CnpjEstabelecimentoVO
            {
                Id = model.Id,
                ImportacaoId = model.ImportacaoId,
                CnpjBasico = model.CnpjBasico,
                CnpjOrdem = model.CnpjOrdem,
                CnpjDv = model.CnpjDv,
                IdentificadorMatrizFilial = model.IdentificadorMatrizFilial,
                NomeFantasia = model.NomeFantasia,
                SituacaoCadastral = model.SituacaoCadastral,
                DataSituacaoCadastral = model.DataSituacaoCadastral,
                MotivoSituacaoCadastralId = model.MotivoSituacaoCadastralId,
                NomeCidadeExterior = model.NomeCidadeExterior,
                PaisId = model.PaisId,
                DataInicioAtividade = model.DataInicioAtividade,
                CnaeId = model.CnaeId,
                TipoLogradouro = model.TipoLogradouro,
                Logradouro = model.Logradouro,
                Numero = model.Numero,
                Complemento = model.Complemento,
                Bairro = model.Bairro,
                Cep = model.Cep,
                Uf = model.Uf,
                MunicipioId = model.MunicipioId,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public CnpjEstabelecimentoListaVO ListFromDomain(CnpjEstabelecimento model)
        {
            return new CnpjEstabelecimentoListaVO
            {
                Id = model.Id,
                ImportacaoId = model.ImportacaoId,
                CnpjBasico = model.CnpjBasico,
                CnpjOrdem = model.CnpjOrdem,
                CnpjDv = model.CnpjDv,
                IdentificadorMatrizFilial = model.IdentificadorMatrizFilial,
                NomeFantasia = model.NomeFantasia,
                SituacaoCadastral = model.SituacaoCadastral,
                DataSituacaoCadastral = model.DataSituacaoCadastral,
                Uf = model.Uf,
                MunicipioId = model.MunicipioId,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        public CnpjEstabelecimento ToDomain()
        {
            return new CnpjEstabelecimento
            {
                Id = this.Id,
                ImportacaoId = this.ImportacaoId,
                CnpjBasico = this.CnpjBasico,
                CnpjOrdem = this.CnpjOrdem,
                CnpjDv = this.CnpjDv,
                IdentificadorMatrizFilial = this.IdentificadorMatrizFilial,
                NomeFantasia = this.NomeFantasia,
                SituacaoCadastral = this.SituacaoCadastral,
                DataSituacaoCadastral = this.DataSituacaoCadastral,
                MotivoSituacaoCadastralId = this.MotivoSituacaoCadastralId,
                NomeCidadeExterior = this.NomeCidadeExterior,
                PaisId = this.PaisId,
                DataInicioAtividade = this.DataInicioAtividade,
                CnaeId = this.CnaeId,
                TipoLogradouro = this.TipoLogradouro,
                Logradouro = this.Logradouro,
                Numero = this.Numero,
                Complemento = this.Complemento,
                Bairro = this.Bairro,
                Cep = this.Cep,
                Uf = this.Uf,
                MunicipioId = this.MunicipioId,
                Status = this.Status,
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }
    }

}
