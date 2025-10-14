using EnergiaApi.Models;

namespace EnergiaApi.Services
{
    public interface IConsumoService
    {
        Task<PagedResult<ConsumoEnergia>> GetConsumosAsync(int pageNumber, int pageSize);
        Task<ConsumoEnergia?> GetByIdAsync(int id);
        Task<ConsumoEnergia> CreateAsync(ConsumoEnergia consumo);
        Task<ConsumoEnergia?> UpdateAsync(int id, ConsumoEnergia consumo);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ConsumoEnergia>> GetByLocalAsync(string local);
        Task<IEnumerable<ConsumoEnergia>> GetByEquipamentoAsync(string equipamento);
        Task<IEnumerable<ConsumoEnergia>> GetByDateRangeAsync(DateTime dataInicio, DateTime dataFim);
        Task<double> GetTotalConsumoByDateRangeAsync(DateTime dataInicio, DateTime dataFim);
        Task<double> GetMediaConsumoByDateRangeAsync(DateTime dataInicio, DateTime dataFim);
    }
}
