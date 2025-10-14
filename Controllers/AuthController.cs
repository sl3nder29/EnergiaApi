using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnergiaApi.Models;
using EnergiaApi.Services;

namespace EnergiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.LoginAsync(request);
            
            if (response == null)
            {
                return Unauthorized(new { message = "Email ou senha inválidos" });
            }

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Usuario>> Register([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(usuario);
            
            if (result == null)
            {
                return BadRequest(new { message = "Email já está em uso" });
            }

            // Não retornar a senha
            result.Senha = string.Empty;
            
            return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
        }

        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> GetUser(int id)
        {
            // Implementar busca do usuário por ID se necessário
            return Ok(new { message = "Endpoint em desenvolvimento" });
        }
    }
}
