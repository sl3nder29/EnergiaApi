using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnergiaApi.Data;

namespace EnergiaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly EnergiaDbContext _context;

        public HealthController(EnergiaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Verifica se a aplicação está funcionando
                var isHealthy = await _context.Database.CanConnectAsync();
                
                if (isHealthy)
                {
                    return Ok(new
                    {
                        status = "Healthy",
                        timestamp = DateTime.UtcNow,
                        database = "Connected"
                    });
                }
                else
                {
                    return StatusCode(503, new
                    {
                        status = "Unhealthy",
                        timestamp = DateTime.UtcNow,
                        database = "Disconnected"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(503, new
                {
                    status = "Unhealthy",
                    timestamp = DateTime.UtcNow,
                    error = ex.Message
                });
            }
        }
    }
}
