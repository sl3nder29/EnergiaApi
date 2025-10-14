using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnergiaApi.Models;
using EnergiaApi.Services;

namespace EnergiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConsumoController : ControllerBase
    {
        private readonly IConsumoService _consumoService;

        public ConsumoController(IConsumoService consumoService)
        {
            _consumoService = consumoService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ConsumoEnergia>>> GetConsumos(
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var result = await _consumoService.GetConsumosAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoEnergia>> GetConsumo(int id)
        {
            var consumo = await _consumoService.GetByIdAsync(id);
            if (consumo == null)
                return NotFound();

            return Ok(consumo);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumoEnergia>> CreateConsumo([FromBody] ConsumoEnergia consumo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _consumoService.CreateAsync(consumo);
            return CreatedAtAction(nameof(GetConsumo), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConsumoEnergia>> UpdateConsumo(int id, [FromBody] ConsumoEnergia consumo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _consumoService.UpdateAsync(id, consumo);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConsumo(int id)
        {
            var success = await _consumoService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("local/{local}")]
        public async Task<ActionResult<IEnumerable<ConsumoEnergia>>> GetByLocal(string local)
        {
            var consumos = await _consumoService.GetByLocalAsync(local);
            return Ok(consumos);
        }

        [HttpGet("equipamento/{equipamento}")]
        public async Task<ActionResult<IEnumerable<ConsumoEnergia>>> GetByEquipamento(string equipamento)
        {
            var consumos = await _consumoService.GetByEquipamentoAsync(equipamento);
            return Ok(consumos);
        }

        [HttpGet("periodo")]
        public async Task<ActionResult<IEnumerable<ConsumoEnergia>>> GetByDateRange(
            [FromQuery] DateTime dataInicio, 
            [FromQuery] DateTime dataFim)
        {
            var consumos = await _consumoService.GetByDateRangeAsync(dataInicio, dataFim);
            return Ok(consumos);
        }

        [HttpGet("total")]
        public async Task<ActionResult<double>> GetTotalConsumo(
            [FromQuery] DateTime dataInicio, 
            [FromQuery] DateTime dataFim)
        {
            var total = await _consumoService.GetTotalConsumoByDateRangeAsync(dataInicio, dataFim);
            return Ok(new { totalConsumo = total });
        }

        [HttpGet("media")]
        public async Task<ActionResult<double>> GetMediaConsumo(
            [FromQuery] DateTime dataInicio, 
            [FromQuery] DateTime dataFim)
        {
            var media = await _consumoService.GetMediaConsumoByDateRangeAsync(dataInicio, dataFim);
            return Ok(new { consumoMedio = media });
        }
    }
}
