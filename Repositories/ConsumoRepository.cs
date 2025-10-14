using Microsoft.EntityFrameworkCore;
using EnergiaApi.Data;
using EnergiaApi.Models;

namespace EnergiaApi.Repositories
{
    public class ConsumoRepository : Repository<ConsumoEnergia>, IConsumoRepository
    {
        public ConsumoRepository(EnergiaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ConsumoEnergia>> GetByLocalAsync(string local)
        {
            return await _dbSet
                .Where(c => c.Local == local)
                .OrderByDescending(c => c.DataHora)
                .ToListAsync();
        }

        public async Task<IEnumerable<ConsumoEnergia>> GetByEquipamentoAsync(string equipamento)
        {
            return await _dbSet
                .Where(c => c.Equipamento == equipamento)
                .OrderByDescending(c => c.DataHora)
                .ToListAsync();
        }

        public async Task<IEnumerable<ConsumoEnergia>> GetByDateRangeAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _dbSet
                .Where(c => c.DataHora >= dataInicio && c.DataHora <= dataFim)
                .OrderByDescending(c => c.DataHora)
                .ToListAsync();
        }

        public async Task<double> GetTotalConsumoByDateRangeAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _dbSet
                .Where(c => c.DataHora >= dataInicio && c.DataHora <= dataFim)
                .SumAsync(c => c.ConsumoKwH);
        }

        public async Task<double> GetMediaConsumoByDateRangeAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _dbSet
                .Where(c => c.DataHora >= dataInicio && c.DataHora <= dataFim)
                .AverageAsync(c => c.ConsumoKwH);
        }
    }
}
