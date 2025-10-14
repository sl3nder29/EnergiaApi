using EnergiaApi.Models;
using EnergiaApi.Repositories;

namespace EnergiaApi.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;

        public SensorService(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public async Task<PagedResult<Sensor>> GetSensoresAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _sensorRepository.CountAsync();
            var sensores = await _sensorRepository.GetAllAsync();
            
            var pagedData = sensores
                .OrderBy(s => s.Nome)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<Sensor>
            {
                Data = pagedData,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Sensor?> GetByIdAsync(int id)
        {
            return await _sensorRepository.GetByIdAsync(id);
        }

        public async Task<Sensor> CreateAsync(Sensor sensor)
        {
            return await _sensorRepository.AddAsync(sensor);
        }

        public async Task<Sensor?> UpdateAsync(int id, Sensor sensor)
        {
            var existingSensor = await _sensorRepository.GetByIdAsync(id);
            if (existingSensor == null)
                return null;

            existingSensor.Nome = sensor.Nome;
            existingSensor.Local = sensor.Local;
            existingSensor.Tipo = sensor.Tipo;
            existingSensor.EstaAtivo = sensor.EstaAtivo;
            existingSensor.ConsumoAtual = sensor.ConsumoAtual;

            await _sensorRepository.UpdateAsync(existingSensor);
            return existingSensor;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id);
            if (sensor == null)
                return false;

            await _sensorRepository.DeleteAsync(sensor);
            return true;
        }

        public async Task<IEnumerable<Sensor>> GetSensoresAtivosAsync()
        {
            return await _sensorRepository.GetSensoresAtivosAsync();
        }

        public async Task<IEnumerable<Sensor>> GetByLocalAsync(string local)
        {
            return await _sensorRepository.GetByLocalAsync(local);
        }

        public async Task<IEnumerable<Sensor>> GetByTipoAsync(string tipo)
        {
            return await _sensorRepository.GetByTipoAsync(tipo);
        }

        public async Task<IEnumerable<Sensor>> DesligarSensoresOciososAsync()
        {
            var sensoresOciosos = await _sensorRepository.GetSensoresOciososAsync();
            
            foreach (var sensor in sensoresOciosos)
            {
                sensor.EstaAtivo = false;
                await _sensorRepository.UpdateAsync(sensor);
            }

            return sensoresOciosos;
        }

        public async Task<int> GetQuantidadeSensoresAtivosAsync()
        {
            return await _sensorRepository.GetQuantidadeSensoresAtivosAsync();
        }
    }
}
