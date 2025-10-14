using EnergiaApi.Models;
using EnergiaApi.Repositories;

namespace EnergiaApi.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IConsumoRepository _consumoRepository;
        private readonly IAlertaRepository _alertaRepository;

        public RelatorioService(IConsumoRepository consumoRepository, IAlertaRepository alertaRepository)
        {
            _consumoRepository = consumoRepository;
            _alertaRepository = alertaRepository;
        }

        public async Task<RelatorioDiario> GetRelatorioDiarioAsync(DateTime data)
        {
            var dataInicio = data.Date;
            var dataFim = data.Date.AddDays(1).AddTicks(-1);

            var consumos = await _consumoRepository.GetByDateRangeAsync(dataInicio, dataFim);
            var alertas = await _alertaRepository.GetByDateRangeAsync(dataInicio, dataFim);

            var consumosList = consumos.ToList();
            var alertasList = alertas.ToList();

            return new RelatorioDiario
            {
                Data = data,
                ConsumoTotal = consumosList.Sum(c => c.ConsumoKwH),
                ConsumoMedio = consumosList.Any() ? consumosList.Average(c => c.ConsumoKwH) : 0,
                ConsumoMaximo = consumosList.Any() ? consumosList.Max(c => c.ConsumoKwH) : 0,
                ConsumoMinimo = consumosList.Any() ? consumosList.Min(c => c.ConsumoKwH) : 0,
                QuantidadeAlertas = alertasList.Count(a => a.EstaAtivo)
            };
        }

        public async Task<RelatorioDiario> GetRelatorioDiarioAsync()
        {
            return await GetRelatorioDiarioAsync(DateTime.Today);
        }

        public async Task<IEnumerable<RelatorioDiario>> GetRelatorioMensalAsync(int ano, int mes)
        {
            var dataInicio = new DateTime(ano, mes, 1);
            var dataFim = dataInicio.AddMonths(1).AddDays(-1);

            var relatorios = new List<RelatorioDiario>();

            for (var data = dataInicio; data <= dataFim; data = data.AddDays(1))
            {
                var relatorio = await GetRelatorioDiarioAsync(data);
                relatorios.Add(relatorio);
            }

            return relatorios;
        }

        public async Task<double> GetConsumoTotalPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _consumoRepository.GetTotalConsumoByDateRangeAsync(dataInicio, dataFim);
        }

        public async Task<double> GetConsumoMedioPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _consumoRepository.GetMediaConsumoByDateRangeAsync(dataInicio, dataFim);
        }
    }
}
