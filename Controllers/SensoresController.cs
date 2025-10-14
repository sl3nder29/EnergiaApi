using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnergiaApi.Models;
using EnergiaApi.Services;

namespace EnergiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SensoresController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensoresController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<Sensor>>> GetSensores(
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var result = await _sensorService.GetSensoresAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("ativos")]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensoresAtivos()
        {
            var sensores = await _sensorService.GetSensoresAtivosAsync();
            return Ok(sensores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetSensor(int id)
        {
            var sensor = await _sensorService.GetByIdAsync(id);
            if (sensor == null)
                return NotFound();

            return Ok(sensor);
        }

        [HttpPost]
        public async Task<ActionResult<Sensor>> CreateSensor([FromBody] Sensor sensor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _sensorService.CreateAsync(sensor);
            return CreatedAtAction(nameof(GetSensor), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Sensor>> UpdateSensor(int id, [FromBody] Sensor sensor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _sensorService.UpdateAsync(id, sensor);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSensor(int id)
        {
            var success = await _sensorService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("local/{local}")]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetByLocal(string local)
        {
            var sensores = await _sensorService.GetByLocalAsync(local);
            return Ok(sensores);
        }

        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetByTipo(string tipo)
        {
            var sensores = await _sensorService.GetByTipoAsync(tipo);
            return Ok(sensores);
        }

        [HttpPost("desligar-ociosos")]
        public async Task<ActionResult<IEnumerable<Sensor>>> DesligarSensoresOciosos()
        {
            var sensoresDesligados = await _sensorService.DesligarSensoresOciososAsync();
            return Ok(sensoresDesligados);
        }

        [HttpGet("quantidade-ativos")]
        public async Task<ActionResult<int>> GetQuantidadeSensoresAtivos()
        {
            var quantidade = await _sensorService.GetQuantidadeSensoresAtivosAsync();
            return Ok(new { quantidadeSensoresAtivos = quantidade });
        }
    }
}
