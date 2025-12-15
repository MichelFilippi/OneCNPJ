using Microsoft.Extensions.Logging;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Presentation;
using OneCNPJ.Infrastructure.Repositories.Interfaces;
using OneCNPJ.Services.Interfaces.GetOne;

namespace OneCNPJ.Services.Services.GetOne
{
    public class FundoGetOneService(
        ICadfiRepository cadfiRepository,
        IConteudoRepository conteudoRepository,
        IRegistroFundoRepository fundoRepository,
        IRegistroClasseRepository classeRepository,
        IRegistroSubclasseRepository subclasseRepository,
        ILogger<FundoGetOneService> logger)
        : IFundoGetOneService
    {
        private readonly ICadfiRepository cadfiRepository = cadfiRepository;
        private readonly IConteudoRepository conteudoRepository = conteudoRepository;
        private readonly IRegistroFundoRepository fundoRepository = fundoRepository;
        private readonly IRegistroClasseRepository classeRepository = classeRepository;
        private readonly IRegistroSubclasseRepository subclasseRepository = subclasseRepository;
        private readonly ILogger<FundoGetOneService> logger = logger;

        public async Task<FundoVO?> GetRegistroPorCnpjAsync(string cnpjFundo, string traceId)
        {
            var cadfi = await cadfiRepository.GetAtualAsync();
            if (cadfi == null)
            {
                logger.LogError("Nenhum registro CADFI atual encontrado. TraceId: {TraceId}", traceId);
                throw new Exception("Nenhum registro CADFI atual encontrado.");
            }

            try
            {
                // Tentar via RegistroFundo
                return await GetRegistroFundo(cnpjFundo, cadfi.Id, traceId);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "RegistroFundo não encontrado para CNPJ: {CnpjFundo} no CADFI Id: {CadfiId}. Tentando via Conteudo. TraceId: {TraceId}", cnpjFundo, cadfi.Id, traceId);
            }

            try
            {
                // Tentar via Conteudo
                return await GetConteudo(cnpjFundo, cadfi.Id, traceId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Conteudo também não encontrado para CNPJ: {CnpjFundo} no CADFI Id: {CadfiId}. TraceId: {TraceId}", cnpjFundo, cadfi.Id, traceId);
                return null;
            }
        }

        private async Task<FundoVO?> GetRegistroFundo(string cnpjFundo, long cadfiId, string traceId)
        {
            try
            {
                var fundo = await fundoRepository.GetRegistroPorCnpjFundoECadfiIdAsync(cnpjFundo, cadfiId);
                if (fundo == null)
                {
                    logger.LogError("Nenhum fundo encontrado. TraceId: {TraceId}", traceId);
                    throw new Exception($"Nenhum fundo encontrado para CNPJ: {cnpjFundo} no CADFI Id: {cadfiId}");
                };

                var classes = await classeRepository.GetTodosOperacionaisPorRegistroFundoIdECadfiIdAsync(fundo.Id, cadfiId);

                if (classes.Any())
                {
                    foreach (var classe in classes)
                    {
                        var subclasses = await subclasseRepository.GetTodosOperacionaisPorRegistroClasseIdECadfiIdAsync(classe.Id, cadfiId);
                        classe.RegistroSubclasses = [.. subclasses];
                    }
                }
                fundo.RegistroClasses = [.. classes];
                return await ConverteRegistroFundoParaFundo(fundo);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao obter RegistroFundo para CNPJ: {CnpjFundo} e CADFI Id: {CadfiId}. TraceId: {TraceId}", cnpjFundo, cadfiId, traceId);
                throw;
            }
        }

        private async Task<FundoVO?> ConverteRegistroFundoParaFundo(RegistroFundoVO registroFundo)
        {
            try
            {
                var newOne = registroFundo.ToFundo();
                List<ClasseVO> classes = [];

                foreach (var classe in registroFundo.RegistroClasses)
                {
                    var newClasse = classe.ToClasse();
                    List<SubclasseVO> subclasses = [];
                    foreach (var subclasse in classe.RegistroSubclasses)
                    {
                        var newSubclasse = subclasse.ToSubclasse();
                        subclasses.Add(newSubclasse);
                    }
                    newClasse.Subclasses = subclasses;
                    classes.Add(newClasse);
                }
                newOne.Classes = classes;

                return await Task.FromResult(newOne);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao converter RegistroFundo para FundoVO. IdRegistroFundo: {IdRegistroFundo}", registroFundo.IdRegistroFundo);
                throw;
            }
        }

        private async Task<FundoVO?> GetConteudo(string cnpjFundo, long cadfiId, string traceId)
        {
            try
            {
                var fundo = await conteudoRepository.GetRegistroPorCnpjFundoECadfiIdAsync(cnpjFundo, cadfiId);
                if (fundo == null)
                {
                    logger.LogError("Nenhum fundo encontrado. TraceId: {TraceId}", traceId);
                    throw new Exception($"Nenhum fundo encontrado para CNPJ: {cnpjFundo} no CADFI Id: {cadfiId}");
                }

                var newOne = fundo.ToFundo();

                return await Task.FromResult(newOne);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao obter Conteudo para CNPJ: {CnpjFundo} e CADFI Id: {CadfiId}. TraceId: {TraceId}", cnpjFundo, cadfiId, traceId);
                throw;
            }
        }
    }
}
