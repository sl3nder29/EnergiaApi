using EnergiaApi.Models;

namespace EnergiaApi.Services
{
    public interface ISensorService
    {
        Task<PagedResult<Sensor>> GetSensoresAsync(int pageNumber, int pageSize);
        Task<Sensor?> GetByIdAsync(int id);
        Task<Sensor> CreateAsync(Sensor sensor);
        Task<Sensor?> UpdateAsync(int id, Sensor sensor);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Sensor>> GetSensoresAtivosAsync();
        Task<IEnumerable<Sensor>> GetByLocalAsync(string local);
        Task<IEnumerable<Sensor>> GetByTipoAsync(string tipo);
        Task<IEnumerable<Sensor>> DesligarSensoresOciososAsync();
        Task<int> GetQuantidadeSensoresAtivosAsync();
    }
}
