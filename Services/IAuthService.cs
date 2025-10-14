using EnergiaApi.Models;

namespace EnergiaApi.Services
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task<Usuario?> RegisterAsync(Usuario usuario);
        string GenerateJwtToken(Usuario usuario);
        bool ValidatePassword(string senha, string hash);
        string HashPassword(string senha);
    }
}
