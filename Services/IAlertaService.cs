using EnergiaApi.Models;

namespace EnergiaApi.Services
{
    public interface IAlertaService
    {
        Task<PagedResult<Alerta>> GetAlertasAsync(int pageNumber, int pageSize);
        Task<Alerta?> GetByIdAsync(int id);
        Task<Alerta> CreateAsync(Alerta alerta);
        Task<Alerta?> UpdateAsync(int id, Alerta alerta);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Alerta>> GetAlertasAtivosAsync();
        Task<IEnumerable<Alerta>> GetByLocalAsync(string local);
        Task<IEnumerable<Alerta>> GetByDateRangeAsync(DateTime dataInicio, DateTime dataFim);
        Task<int> GetQuantidadeAlertasAtivosAsync();
    }
}
