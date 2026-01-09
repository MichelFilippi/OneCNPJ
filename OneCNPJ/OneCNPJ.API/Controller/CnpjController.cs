using Microsoft.AspNetCore.Mvc;
using OneCNPJ.Common.Utilities;
using OneCNPJ.Domain.Models;
using OneCNPJ.DTOs.VOs;
using OneCNPJ.DTOs.VOs.Presentation;
using OneCNPJ.Services.Interfaces;
using OneCNPJ.Services.Interfaces.Import;

namespace OneCNPJ.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CnpjController(
        ICnpjImportacaoGetOneService cnpjGetOneService,
        ICnpjImportService cnpjImportService,
        ILogger<CnpjController> logger)
        : Controller
    {
        private readonly ICnpjImportacaoGetOneService _cnpjGetOneService = cnpjGetOneService;
        private readonly ICnpjImportService _cnpjImportService = cnpjImportService;
        private readonly ILogger<CnpjController> logger = logger;

        /// <summary>
        /// Inicia o processo de importação dos dados da RFD.
        /// </summary>
        [HttpPost("[action]")]
        public async Task<ActionResult<CnpjImportacaoVO>> Importar()
        {
            var traceId = Guid.NewGuid().ToString();
            logger.LogInformation("Iniciando importação CNPJ da RFD. TraceId: {TraceId}", traceId);

            try
            {
                var response = await _cnpjImportService.ImportarDaRfbAsync(traceId);
                logger.LogInformation("Importação concluída com sucesso. TraceId: {TraceId}", traceId);
                if (response == null)
                {
                    logger.LogWarning("Importação CNPJ retornou nulo. TraceId: {TraceId}", traceId);
                    return BadRequest("Não foi possível iniciar o processo de importação.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao importar CNPJ da RFD. TraceId: {TraceId}", traceId);
                return StatusCode(500, $"Erro interno ao importar: {ex.Message}");
            }
        }

        /// <summary>
        /// Retornar o status atual do serviço de CNPJ
        /// </summary>
        [HttpGet("[action]")]
        public async Task<ActionResult<CnpjImportacaoVO>> GetStatusAsync()
        {
            try
            {
                var response = await _cnpjGetOneService.GetStatusAsync();
                if (response == null)
                {
                    return BadRequest("Não foi possível obter o status do serviço.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar status do serviço: {ex.Message}");
            }
        }

        /// <summary>
        /// Retornar o registro atual (corrente) do serviço de CNPJ
        /// </summary>
        [HttpGet("[action]")]
        public async Task<ActionResult<CnpjImportacaoVO>> GetAtualAsync()
        {
            try
            {
                var response = await _cnpjGetOneService.GetAtualAsync();
                if (response == null)
                {
                    return BadRequest("Não foi possível obter o registro atual.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar registro atual: {ex.Message}");
            }
        }

        /// <summary>
        /// Retornar o registro atual (corrente) do CNPJ informado
        /// </summary>
        [HttpGet("[action]/{cnpj}")]
        public async Task<ActionResult<CnpjImportacaoStatusVO>> GetCnpjAsync(string cnpj)
        {
            var traceId = Guid.NewGuid().ToString();
            logger.LogInformation("Iniciando localização do cnpj. TraceId: {TraceId} {cnpj}", traceId, cnpj);

            try
            {
                if (cnpj.Length == 14 && !cnpj.IsCnpj())
                {
                    logger.LogInformation("CNPJ inválido. TraceId: {TraceId} {cnpj}", traceId, cnpj);
                    return BadRequest(new { erro = $"CNPJ inválido {cnpj}" });
                }

                var result = await cnpjGetOneService.GetPorCnpjAsync(cnpj, traceId);

                if (result == null)
                {
                    logger.LogInformation("CNPJ não localizado. TraceId: {TraceId} {cnpj}", traceId, cnpj);
                    return BadRequest(new { erro = $"Cnpj não localizado {cnpj}" });
                }
                return Ok(result);
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao localizar cnpj. TraceId: {TraceId} {cnpj}", traceId, cnpj);
                return StatusCode(500, $"Erro interno ao buscar registro atual: {ex.Message}");
            }
        }
    }
}
