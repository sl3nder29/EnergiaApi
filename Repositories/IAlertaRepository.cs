using EnergiaApi.Models;

namespace EnergiaApi.Repositories
{
    public interface IAlertaRepository : IRepository<Alerta>
    {
        Task<IEnumerable<Alerta>> GetAlertasAtivosAsync();
        Task<IEnumerable<Alerta>> GetByLocalAsync(string local);
        Task<IEnumerable<Alerta>> GetByDateRangeAsync(DateTime dataInicio, DateTime dataFim);
        Task<int> GetQuantidadeAlertasAtivosAsync();
    }
}
