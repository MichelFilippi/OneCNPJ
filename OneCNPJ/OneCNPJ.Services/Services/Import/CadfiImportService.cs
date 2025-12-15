using Microsoft.Extensions.Logging;
using OneCNPJ.Common.Enums;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.Infrastructure.Repositories.Interfaces;
using OneCNPJ.Services.Interfaces.Import;

namespace OneCNPJ.Services.Services.Import
{
    public class CadfiImportService(
        ICadfiRepository repository,
        IConteudoImportService conteudoImportService,
        IRegistroFundoImportService fundoImportService,
        IRegistroClasseImportService classeImportService,
        IRegistroSubclasseImportService subclasseImportService,
        ILogger<CadfiImportService> logger)
        : ICadfiImportService
    {
        private readonly ICadfiRepository repository = repository;
        private readonly IConteudoImportService conteudoImportService = conteudoImportService;
        private readonly IRegistroFundoImportService fundoImportService = fundoImportService;
        private readonly IRegistroClasseImportService classeImportService = classeImportService;
        private readonly IRegistroSubclasseImportService subclasseImportService = subclasseImportService;
        private readonly ILogger<CadfiImportService> logger = logger;

        public async Task<CadfiVO?> ImportarDaCvmAsync(string traceId)
        {
            try
            {
                logger.LogInformation("Processamento iniciado. TraceId: {TraceId}", traceId);
                var atual = await repository.GetUltimoAsync();
                if (atual != null && atual.Status == StatusEnum.Processamento )
                {
                    logger.LogWarning("Processamento abortado. TraceId: {TraceId}. Importação em andamento.", traceId);
                    throw new Exception("Existe um processo de importação CADFI em andamento.");
                }

                CadfiVO entity = new(traceId);
                var response = await repository.Create(entity);
                if (response == null)
                {
                    logger.LogWarning("Falha ao criar registro CADFI. TraceId: {TraceId}", traceId);
                    return response;
                }

                _ = Task.Run(() => ImportProcess(response, traceId));

                logger.LogInformation("Processo de importação disparado em background. TraceId: {TraceId}", traceId);

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro inesperado ao iniciar importação CADFI. TraceId: {TraceId}", traceId);
                throw;
            }
        }

        private async Task<CadfiVO> ImportProcess(CadfiVO entity, string traceId)
        {
            try
            {
                logger.LogInformation("Etapa Conteúdo iniciada. TraceId: {TraceId}", traceId);
                await conteudoImportService.ImportarDaCvmAsync(entity);
                logger.LogInformation("Etapa Conteúdo finalizada. TraceId: {TraceId}", traceId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro na etapa Conteúdo. TraceId: {TraceId}", traceId);
                return await FinalizarImportacaoAsync(entity.Id, traceId);
            }

            try
            {
                logger.LogInformation("Etapa Fundo iniciada. TraceId: {TraceId}", traceId);
                await fundoImportService.ImportarDaCvmAsync(entity);
                logger.LogInformation("Etapa Fundo finalizada. TraceId: {TraceId}", traceId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro na etapa Fundo. TraceId: {TraceId}", traceId);
                return await FinalizarImportacaoAsync(entity.Id, traceId);
            }

            try
            {
                logger.LogInformation("Etapa Classe iniciada. TraceId: {TraceId}", traceId);
                await classeImportService.ImportarDaCvmAsync(entity);
                logger.LogInformation("Etapa Classe finalizada. TraceId: {TraceId}", traceId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro na etapa Classe. TraceId: {TraceId}", traceId);
                return await FinalizarImportacaoAsync(entity.Id, traceId);
            }

            try
            {
                logger.LogInformation("Etapa Subclasse iniciada. TraceId: {TraceId}", traceId);
                await subclasseImportService.ImportarDaCvmAsync(entity);
                logger.LogInformation("Etapa Subclasse finalizada. TraceId: {TraceId}", traceId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro na etapa Subclasse. TraceId: {TraceId}", traceId);
                return await FinalizarImportacaoAsync(entity.Id, traceId);
            }

            logger.LogInformation("Processamento completo. TraceId: {TraceId}", traceId);
            return await FinalizarImportacaoAsync(entity.Id, traceId);
        }

        private async Task<CadfiVO> FinalizarImportacaoAsync(long entityId, string traceId)
        {
            var entity = await repository.GetRegistroPorIdAsync(entityId);
            if (entity == null)
            {
                logger.LogError("Erro ao obter o registro atualizado. TraceId: {TraceId}", traceId);
                throw new Exception("Erro ao obter o registro atualizado.");
            }

            StatusEnum status = StatusEnum.ImportacaoOk;
            if (entity.StatusNaoAdaptados175 != StatusEnum.ImportacaoOk ||
                entity.StatusRegistroFundo != StatusEnum.ImportacaoOk ||
                entity.StatusRegistroClasse != StatusEnum.ImportacaoOk ||
                entity.StatusRegistroSubclasse != StatusEnum.ImportacaoOk)
            {
                status = StatusEnum.ImportacaoErro;
                logger.LogWarning("Importação finalizada com erro. TraceId: {TraceId}", traceId);
            }
            else
            {
                logger.LogInformation("Importação finalizada com sucesso. TraceId: {TraceId}", traceId);
            }

            entity.DataAtualizacao = DateTime.UtcNow;
            entity.Status = status;
            var updatedEntity = await repository.Update(entity);
            if (updatedEntity == null)
            {
                logger.LogError("Erro ao finalizar o processo de importação. TraceId: {TraceId}", traceId);
                throw new Exception("Erro ao finalizar o processo de importação.");
            }
            return updatedEntity;
        }
    }
}