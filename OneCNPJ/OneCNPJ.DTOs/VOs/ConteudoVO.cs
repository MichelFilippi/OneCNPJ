using System.Text.Json;
using OneCNPJ.Common.Enums;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs.Listas;

namespace OneCNPJ.DTOs.VOs
{
    public class ConteudoVO : BaseVO, IEntityVO<Conteudo, ConteudoVO, ConteudoListaVO>
    {
        public long ImportacaoId { get; set; }
        public string Cnpj { get; set; } = string.Empty;

        // O “conteúdo” que você quer imprimir:
        public CnpjImportacaoVO? Importacao { get; set; }
        public CnpjEmpresaVO? Empresa { get; set; }

        // redundância útil pro client (opcional)
        public IEnumerable<CnpjEstabelecimentoVO> Estabelecimentos =>
            Empresa?.Estabelecimentos ?? [];

        public IEnumerable<CnpjSocioVO> Socios =>
            Empresa?.Socios ?? [];

        public CnpjSimplesVO? Simples =>
            Empresa?.Simples;

        public ConteudoVO() { }

        // Converte Domain->VO
        public ConteudoVO FromDomain(Conteudo model)
        {
            var vo = new ConteudoVO
            {
                Id = model.Id,
                ImportacaoId = model.ImportacaoId,
                Cnpj = model.Cnpj,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };

            // tenta “hidratar” o payload salvo
            if (!string.IsNullOrWhiteSpace(model.PayloadJson) && model.PayloadJson != "{}")
            {
                try
                {
                    var parsed = JsonSerializer.Deserialize<ConteudoPayload>(model.PayloadJson, JsonOpts());
                    vo.Importacao = parsed?.Importacao;
                    vo.Empresa = parsed?.Empresa;
                }
                catch
                {
                    // se quebrar, continua “vazio” (você pode logar no repository/service)
                }
            }

            return vo;
        }

        // Para listas (se quiser)
        public ConteudoListaVO ListFromDomain(Conteudo model)
        {
            return new ConteudoListaVO
            {
                Id = model.Id,
                ImportacaoId = model.ImportacaoId,
                Cnpj = model.Cnpj,
                Status = model.Status,
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao
            };
        }

        // Converte VO->Domain (salvando o payload_json)
        public Conteudo ToDomain()
        {
            var payload = new ConteudoPayload
            {
                Importacao = this.Importacao,
                Empresa = this.Empresa
            };

            return new Conteudo
            {
                Id = this.Id,
                ImportacaoId = this.ImportacaoId,
                Cnpj = this.Cnpj,
                Status = this.Status,
                PayloadJson = JsonSerializer.Serialize(payload, JsonOpts()),
                DataCriacao = this.DataCriacao,
                DataAtualizacao = this.DataAtualizacao
            };
        }

        private static JsonSerializerOptions JsonOpts() => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        // “Envelope” do payload_json
        private sealed class ConteudoPayload
        {
            public CnpjImportacaoVO? Importacao { get; set; }
            public CnpjEmpresaVO? Empresa { get; set; }
        }
    }
}
