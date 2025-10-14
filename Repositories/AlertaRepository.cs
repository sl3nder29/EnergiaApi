using Microsoft.EntityFrameworkCore;
using EnergiaApi.Data;
using EnergiaApi.Models;

namespace EnergiaApi.Repositories
{
    public class AlertaRepository : Repository<Alerta>, IAlertaRepository
    {
        public AlertaRepository(EnergiaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Alerta>> GetAlertasAtivosAsync()
        {
            return await _dbSet
                .Where(a => a.EstaAtivo)
                .OrderByDescending(a => a.DataHora)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alerta>> GetByLocalAsync(string local)
        {
            return await _dbSet
                .Where(a => a.Local == local)
                .OrderByDescending(a => a.DataHora)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alerta>> GetByDateRangeAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _dbSet
                .Where(a => a.DataHora >= dataInicio && a.DataHora <= dataFim)
                .OrderByDescending(a => a.DataHora)
                .ToListAsync();
        }

        public async Task<int> GetQuantidadeAlertasAtivosAsync()
        {
            return await _dbSet
                .CountAsync(a => a.EstaAtivo);
        }
    }
}
