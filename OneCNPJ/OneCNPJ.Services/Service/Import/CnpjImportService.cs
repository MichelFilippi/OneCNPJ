using Microsoft.Extensions.Logging;
using OneCNPJ.Common.Enums;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.Infrastructure.Repository.Interfaces;
using OneCNPJ.Services.Interfaces.Import;
using OneCNPJ.Services.Service.Import;

namespace OneCNPJ.Services.Services.Import
{
    public class CnpjImportService(
        ICnpjImportacaoRepository importacaoRepository,
        ICnpjEmpresasImportStep empresasStep,
        ICnpjEstabelecimentosImportStep estabelecimentosStep,
        ICnpjSociosImportStep sociosStep,
        ICnpjSimplesImportStep simplesStep,
        ICnpjSatelitesImportStep satelitesStep,
        ILogger<CnpjImportService> logger
    ) : ICnpjImportService
    {
        private readonly ICnpjImportacaoRepository _importacaoRepository = importacaoRepository;
        private readonly ICnpjEmpresasImportStep _empresasStep = empresasStep;
        private readonly ICnpjEstabelecimentosImportStep _estabelecimentosStep = estabelecimentosStep;
        private readonly ICnpjSociosImportStep _sociosStep = sociosStep;
        private readonly ICnpjSimplesImportStep _simplesStep = simplesStep;
        private readonly ICnpjSatelitesImportStep _satelitesStep = satelitesStep;
        private readonly ILogger<CnpjImportService> _logger = logger;

        public async Task<CnpjImportacaoVO?> ImportarDaRfbAsync(string traceId)
        {
            try
            {
                _logger.LogInformation("Processamento CNPJ iniciado. TraceId: {TraceId}", traceId);

                var ultimo = await _importacaoRepository.GetUltimoAsync();
                if (ultimo != null && ultimo.Status == StatusEnum.Processamento)
                {
                    _logger.LogWarning("Processamento abortado. TraceId: {TraceId}. Importação em andamento.", traceId);
                    throw new Exception("Existe um processo de importação CNPJ em andamento.");
                }

                // Cria o “lote”
                var nova = new CnpjImportacaoVO
                {
                    InicioEm = DateTime.UtcNow,
                    Status = StatusEnum.Processamento,
                    StatusEmpresas = StatusEnum.Processamento,
                    StatusEstabelecimentos = StatusEnum.Processamento,
                    StatusSocios = StatusEnum.Processamento,
                    StatusSimples = StatusEnum.Processamento,
                    StatusSatelites = StatusEnum.Processamento,
                };

                var created = await _importacaoRepository.Create(nova);
                if (created == null)
                {
                    _logger.LogWarning("Falha ao criar registro CNPJImportacao. TraceId: {TraceId}", traceId);
                    return null;
                }

                // Dispara background (padrão do CADFI)
                _ = Task.Run(() => ImportProcess(created.Id, traceId));

                _logger.LogInformation("Processo de importação CNPJ disparado em background. TraceId: {TraceId}", traceId);
                return created;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao iniciar importação CNPJ. TraceId: {TraceId}", traceId);
                throw;
            }
        }

        private async Task ImportProcess(long importacaoId, string traceId)
        {
            var importacao = await _importacaoRepository.GetRegistroPorIdAsync(importacaoId);
            if (importacao == null)
            {
                _logger.LogError("Lote não encontrado para processamento. TraceId: {TraceId} Id: {Id}", traceId, importacaoId);
                return;
            }

            try
            {
                // 👉 Atualiza origem antes de começar (pasta/versão)
                await PreencherOrigemAsync(importacaoId, traceId);

                _logger.LogInformation("Etapa Satélites iniciada. TraceId: {TraceId}", traceId);
                await _satelitesStep.ImportarAsync(importacao, traceId);
                _logger.LogInformation("Etapa Satélites finalizada. TraceId: {TraceId}", traceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro na etapa Satélites. TraceId: {TraceId}", traceId);
                await FinalizarImportacaoAsync(importacaoId, traceId);
                return;
            }

            try
            {
                _logger.LogInformation("Etapa Empresas iniciada. TraceId: {TraceId}", traceId);
                await _empresasStep.ImportarAsync(importacao, traceId);
                _logger.LogInformation("Etapa Empresas finalizada. TraceId: {TraceId}", traceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro na etapa Empresas. TraceId: {TraceId}", traceId);
                await FinalizarImportacaoAsync(importacaoId, traceId);
                return;
            }

            try
            {
                _logger.LogInformation("Etapa Estabelecimentos iniciada. TraceId: {TraceId}", traceId);
                await _estabelecimentosStep.ImportarAsync(importacao, traceId);
                _logger.LogInformation("Etapa Estabelecimentos finalizada. TraceId: {TraceId}", traceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro na etapa Estabelecimentos. TraceId: {TraceId}", traceId);
                await FinalizarImportacaoAsync(importacaoId, traceId);
                return;
            }

            try
            {
                _logger.LogInformation("Etapa Sócios iniciada. TraceId: {TraceId}", traceId);
                await _sociosStep.ImportarAsync(importacao, traceId);
                _logger.LogInformation("Etapa Sócios finalizada. TraceId: {TraceId}", traceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro na etapa Sócios. TraceId: {TraceId}", traceId);
                await FinalizarImportacaoAsync(importacaoId, traceId);
                return;
            }

            try
            {
                _logger.LogInformation("Etapa Simples iniciada. TraceId: {TraceId}", traceId);
                await _simplesStep.ImportarAsync(importacao, traceId);
                _logger.LogInformation("Etapa Simples finalizada. TraceId: {TraceId}", traceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro na etapa Simples. TraceId: {TraceId}", traceId);
                await FinalizarImportacaoAsync(importacaoId, traceId);
                return;
            }

            await FinalizarImportacaoAsync(importacaoId, traceId);
        }

        private async Task PreencherOrigemAsync(long importacaoId, string traceId)
        {
            // pega a última pasta YYYY-MM a partir do helper
            using var http = new HttpClient();

            // Reaproveita a lógica do helper: se quiser, crie um método GetLatestVersionAsync
            // Aqui vou reusar o ListZipLinksAsync e extrair a pasta pelo 1º zip
            var zips = await RfbOpenDataHelper.ListZipLinksAsync(http, traceId);
            var first = new Uri(zips[0]);

            // .../dados_abertos_cnpj/2025-12/Empresas0.zip
            var parts = first.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var versao = parts[^2]; // "2025-12"
            var url = $"{first.Scheme}://{first.Host}/{string.Join('/', parts.Take(parts.Length - 1))}/";

            var imp = await _importacaoRepository.GetRegistroPorIdAsync(importacaoId);
            if (imp == null) return;

            imp.VersaoOrigem = versao;
            imp.UrlOrigem = url;

            // DataReferencia como primeiro dia do mês
            if (versao.Length == 7 &&
                int.TryParse(versao.Substring(0, 4), out var y) &&
                int.TryParse(versao.Substring(5, 2), out var m))
            {
                imp.DataReferencia = new DateTime(y, m, 1, 0, 0, 0, DateTimeKind.Utc);
            }

            imp.DataAtualizacao = DateTime.UtcNow;

            await _importacaoRepository.Update(imp);
        }

        private async Task FinalizarImportacaoAsync(long importacaoId, string traceId)
        {
            var entity = await _importacaoRepository.GetRegistroPorIdAsync(importacaoId);
            if (entity == null)
            {
                _logger.LogError("Erro ao obter registro atualizado do lote. TraceId: {TraceId}", traceId);
                return;
            }

            // Regra: se qualquer dataset falhou → ImportacaoErro
            var ok =
                entity.StatusEmpresas == StatusEnum.ImportacaoOk &&
                entity.StatusEstabelecimentos == StatusEnum.ImportacaoOk &&
                entity.StatusSocios == StatusEnum.ImportacaoOk &&
                entity.StatusSimples == StatusEnum.ImportacaoOk &&
                entity.StatusSatelites == StatusEnum.ImportacaoOk;

            entity.FimEm = DateTime.UtcNow;
            entity.DataAtualizacao = DateTime.UtcNow;
            entity.Status = ok ? StatusEnum.ImportacaoOk : StatusEnum.ImportacaoErro;

            await _importacaoRepository.Update(entity);

            if (ok)
                _logger.LogInformation("Importação CNPJ finalizada com sucesso. TraceId: {TraceId}", traceId);
            else
                _logger.LogWarning("Importação CNPJ finalizada com erro. TraceId: {TraceId}", traceId);
        }
    }
}
