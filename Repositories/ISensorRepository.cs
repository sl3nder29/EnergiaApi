using EnergiaApi.Models;

namespace EnergiaApi.Repositories
{
    public interface ISensorRepository : IRepository<Sensor>
    {
        Task<IEnumerable<Sensor>> GetSensoresAtivosAsync();
        Task<IEnumerable<Sensor>> GetByLocalAsync(string local);
        Task<IEnumerable<Sensor>> GetByTipoAsync(string tipo);
        Task<IEnumerable<Sensor>> GetSensoresOciososAsync();
        Task<int> GetQuantidadeSensoresAtivosAsync();
    }
}
