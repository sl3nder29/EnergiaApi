using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnergiaApi.Models;
using EnergiaApi.Services;

namespace EnergiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatoriosController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("diario")]
        public async Task<ActionResult<RelatorioDiario>> GetRelatorioDiario()
        {
            var relatorio = await _relatorioService.GetRelatorioDiarioAsync();
            return Ok(relatorio);
        }

        [HttpGet("diario/{data}")]
        public async Task<ActionResult<RelatorioDiario>> GetRelatorioDiario(DateTime data)
        {
            var relatorio = await _relatorioService.GetRelatorioDiarioAsync(data);
            return Ok(relatorio);
        }

        [HttpGet("mensal/{ano}/{mes}")]
        public async Task<ActionResult<IEnumerable<RelatorioDiario>>> GetRelatorioMensal(int ano, int mes)
        {
            if (ano < 2000 || ano > 2100)
                return BadRequest("Ano inválido");

            if (mes < 1 || mes > 12)
                return BadRequest("Mês inválido");

            var relatorios = await _relatorioService.GetRelatorioMensalAsync(ano, mes);
            return Ok(relatorios);
        }

        [HttpGet("total-periodo")]
        public async Task<ActionResult<double>> GetConsumoTotalPorPeriodo(
            [FromQuery] DateTime dataInicio, 
            [FromQuery] DateTime dataFim)
        {
            if (dataInicio > dataFim)
                return BadRequest("Data de início deve ser anterior à data de fim");

            var total = await _relatorioService.GetConsumoTotalPorPeriodoAsync(dataInicio, dataFim);
            return Ok(new { consumoTotal = total });
        }

        [HttpGet("media-periodo")]
        public async Task<ActionResult<double>> GetConsumoMedioPorPeriodo(
            [FromQuery] DateTime dataInicio, 
            [FromQuery] DateTime dataFim)
        {
            if (dataInicio > dataFim)
                return BadRequest("Data de início deve ser anterior à data de fim");

            var media = await _relatorioService.GetConsumoMedioPorPeriodoAsync(dataInicio, dataFim);
            return Ok(new { consumoMedio = media });
        }
    }
}
