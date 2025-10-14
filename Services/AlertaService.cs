using EnergiaApi.Models;
using EnergiaApi.Repositories;

namespace EnergiaApi.Services
{
    public class AlertaService : IAlertaService
    {
        private readonly IAlertaRepository _alertaRepository;

        public AlertaService(IAlertaRepository alertaRepository)
        {
            _alertaRepository = alertaRepository;
        }

        public async Task<PagedResult<Alerta>> GetAlertasAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _alertaRepository.CountAsync();
            var alertas = await _alertaRepository.GetAllAsync();
            
            var pagedData = alertas
                .OrderByDescending(a => a.DataHora)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<Alerta>
            {
                Data = pagedData,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Alerta?> GetByIdAsync(int id)
        {
            return await _alertaRepository.GetByIdAsync(id);
        }

        public async Task<Alerta> CreateAsync(Alerta alerta)
        {
            alerta.DataHora = DateTime.UtcNow;
            return await _alertaRepository.AddAsync(alerta);
        }

        public async Task<Alerta?> UpdateAsync(int id, Alerta alerta)
        {
            var existingAlerta = await _alertaRepository.GetByIdAsync(id);
            if (existingAlerta == null)
                return null;

            existingAlerta.Mensagem = alerta.Mensagem;
            existingAlerta.Local = alerta.Local;
            existingAlerta.ConsumoAtual = alerta.ConsumoAtual;
            existingAlerta.Limite = alerta.Limite;
            existingAlerta.EstaAtivo = alerta.EstaAtivo;

            await _alertaRepository.UpdateAsync(existingAlerta);
            return existingAlerta;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alerta = await _alertaRepository.GetByIdAsync(id);
            if (alerta == null)
                return false;

            await _alertaRepository.DeleteAsync(alerta);
            return true;
        }

        public async Task<IEnumerable<Alerta>> GetAlertasAtivosAsync()
        {
            return await _alertaRepository.GetAlertasAtivosAsync();
        }

        public async Task<IEnumerable<Alerta>> GetByLocalAsync(string local)
        {
            return await _alertaRepository.GetByLocalAsync(local);
        }

        public async Task<IEnumerable<Alerta>> GetByDateRangeAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _alertaRepository.GetByDateRangeAsync(dataInicio, dataFim);
        }

        public async Task<int> GetQuantidadeAlertasAtivosAsync()
        {
            return await _alertaRepository.GetQuantidadeAlertasAtivosAsync();
        }
    }
}
