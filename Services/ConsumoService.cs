using EnergiaApi.Models;
using EnergiaApi.Repositories;

namespace EnergiaApi.Services
{
    public class ConsumoService : IConsumoService
    {
        private readonly IConsumoRepository _consumoRepository;

        public ConsumoService(IConsumoRepository consumoRepository)
        {
            _consumoRepository = consumoRepository;
        }

        public async Task<PagedResult<ConsumoEnergia>> GetConsumosAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _consumoRepository.CountAsync();
            var consumos = await _consumoRepository.GetAllAsync();
            
            var pagedData = consumos
                .OrderByDescending(c => c.DataHora)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<ConsumoEnergia>
            {
                Data = pagedData,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ConsumoEnergia?> GetByIdAsync(int id)
        {
            return await _consumoRepository.GetByIdAsync(id);
        }

        public async Task<ConsumoEnergia> CreateAsync(ConsumoEnergia consumo)
        {
            consumo.DataHora = DateTime.UtcNow;
            return await _consumoRepository.AddAsync(consumo);
        }

        public async Task<ConsumoEnergia?> UpdateAsync(int id, ConsumoEnergia consumo)
        {
            var existingConsumo = await _consumoRepository.GetByIdAsync(id);
            if (existingConsumo == null)
                return null;

            existingConsumo.Local = consumo.Local;
            existingConsumo.Equipamento = consumo.Equipamento;
            existingConsumo.ConsumoKwH = consumo.ConsumoKwH;
            existingConsumo.EstaAtivo = consumo.EstaAtivo;

            await _consumoRepository.UpdateAsync(existingConsumo);
            return existingConsumo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var consumo = await _consumoRepository.GetByIdAsync(id);
            if (consumo == null)
                return false;

            await _consumoRepository.DeleteAsync(consumo);
            return true;
        }

        public async Task<IEnumerable<ConsumoEnergia>> GetByLocalAsync(string local)
        {
            return await _consumoRepository.GetByLocalAsync(local);
        }

        public async Task<IEnumerable<ConsumoEnergia>> GetByEquipamentoAsync(string equipamento)
        {
            return await _consumoRepository.GetByEquipamentoAsync(equipamento);
        }

        public async Task<IEnumerable<ConsumoEnergia>> GetByDateRangeAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _consumoRepository.GetByDateRangeAsync(dataInicio, dataFim);
        }

        public async Task<double> GetTotalConsumoByDateRangeAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _consumoRepository.GetTotalConsumoByDateRangeAsync(dataInicio, dataFim);
        }

        public async Task<double> GetMediaConsumoByDateRangeAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _consumoRepository.GetMediaConsumoByDateRangeAsync(dataInicio, dataFim);
        }
    }
}
