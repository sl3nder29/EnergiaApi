using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EnergiaApi.Models;
using EnergiaApi.Repositories;

namespace EnergiaApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(request.Email);
            
            if (usuario == null || !ValidatePassword(request.Senha, usuario.Senha))
            {
                return null;
            }

            if (!usuario.EstaAtivo)
            {
                return null;
            }

            var token = GenerateJwtToken(usuario);
            var expiresAt = DateTime.UtcNow.AddHours(24);

            return new LoginResponse
            {
                Token = token,
                ExpiresAt = expiresAt,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public async Task<Usuario?> RegisterAsync(Usuario usuario)
        {
            if (await _usuarioRepository.ExistsByEmailAsync(usuario.Email))
            {
                return null;
            }

            usuario.Senha = HashPassword(usuario.Senha);
            usuario.DataCriacao = DateTime.UtcNow;
            usuario.EstaAtivo = true;

            return await _usuarioRepository.AddAsync(usuario);
        }

        public string GenerateJwtToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidatePassword(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }

        public string HashPassword(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }
    }
}
