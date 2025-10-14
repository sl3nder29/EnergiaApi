using Microsoft.EntityFrameworkCore;
using EnergiaApi.Data;
using EnergiaApi.Models;

namespace EnergiaApi.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(EnergiaDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbSet
                .AnyAsync(u => u.Email == email);
        }
    }
}
