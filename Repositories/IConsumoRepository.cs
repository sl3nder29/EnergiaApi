using EnergiaApi.Models;

namespace EnergiaApi.Repositories
{
    public interface IConsumoRepository : IRepository<ConsumoEnergia>
    {
        Task<IEnumerable<ConsumoEnergia>> GetByLocalAsync(string local);
        Task<IEnumerable<ConsumoEnergia>> GetByEquipamentoAsync(string equipamento);
        Task<IEnumerable<ConsumoEnergia>> GetByDateRangeAsync(DateTime dataInicio, DateTime dataFim);
        Task<double> GetTotalConsumoByDateRangeAsync(DateTime dataInicio, DateTime dataFim);
        Task<double> GetMediaConsumoByDateRangeAsync(DateTime dataInicio, DateTime dataFim);
    }
}
