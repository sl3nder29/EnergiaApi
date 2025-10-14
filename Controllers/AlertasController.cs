using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnergiaApi.Models;
using EnergiaApi.Services;

namespace EnergiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AlertasController : ControllerBase
    {
        private readonly IAlertaService _alertaService;

        public AlertasController(IAlertaService alertaService)
        {
            _alertaService = alertaService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<Alerta>>> GetAlertas(
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var result = await _alertaService.GetAlertasAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("ativos")]
        public async Task<ActionResult<IEnumerable<Alerta>>> GetAlertasAtivos()
        {
            var alertas = await _alertaService.GetAlertasAtivosAsync();
            return Ok(alertas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alerta>> GetAlerta(int id)
        {
            var alerta = await _alertaService.GetByIdAsync(id);
            if (alerta == null)
                return NotFound();

            return Ok(alerta);
        }

        [HttpPost]
        public async Task<ActionResult<Alerta>> CreateAlerta([FromBody] Alerta alerta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _alertaService.CreateAsync(alerta);
            return CreatedAtAction(nameof(GetAlerta), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Alerta>> UpdateAlerta(int id, [FromBody] Alerta alerta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _alertaService.UpdateAsync(id, alerta);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAlerta(int id)
        {
            var success = await _alertaService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("local/{local}")]
        public async Task<ActionResult<IEnumerable<Alerta>>> GetByLocal(string local)
        {
            var alertas = await _alertaService.GetByLocalAsync(local);
            return Ok(alertas);
        }

        [HttpGet("periodo")]
        public async Task<ActionResult<IEnumerable<Alerta>>> GetByDateRange(
            [FromQuery] DateTime dataInicio, 
            [FromQuery] DateTime dataFim)
        {
            var alertas = await _alertaService.GetByDateRangeAsync(dataInicio, dataFim);
            return Ok(alertas);
        }

        [HttpGet("quantidade-ativos")]
        public async Task<ActionResult<int>> GetQuantidadeAlertasAtivos()
        {
            var quantidade = await _alertaService.GetQuantidadeAlertasAtivosAsync();
            return Ok(new { quantidadeAlertasAtivos = quantidade });
        }
    }
}
