using Microsoft.EntityFrameworkCore;
using EnergiaApi.Data;
using EnergiaApi.Models;

namespace EnergiaApi.Repositories
{
    public class SensorRepository : Repository<Sensor>, ISensorRepository
    {
        public SensorRepository(EnergiaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sensor>> GetSensoresAtivosAsync()
        {
            return await _dbSet
                .Where(s => s.EstaAtivo)
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sensor>> GetByLocalAsync(string local)
        {
            return await _dbSet
                .Where(s => s.Local == local)
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sensor>> GetByTipoAsync(string tipo)
        {
            return await _dbSet
                .Where(s => s.Tipo == tipo)
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sensor>> GetSensoresOciososAsync()
        {
            return await _dbSet
                .Where(s => s.ConsumoAtual == 0 && s.EstaAtivo)
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }

        public async Task<int> GetQuantidadeSensoresAtivosAsync()
        {
            return await _dbSet
                .CountAsync(s => s.EstaAtivo);
        }
    }
}
