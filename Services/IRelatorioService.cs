using EnergiaApi.Models;

namespace EnergiaApi.Services
{
    public interface IRelatorioService
    {
        Task<RelatorioDiario> GetRelatorioDiarioAsync(DateTime data);
        Task<RelatorioDiario> GetRelatorioDiarioAsync();
        Task<IEnumerable<RelatorioDiario>> GetRelatorioMensalAsync(int ano, int mes);
        Task<double> GetConsumoTotalPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<double> GetConsumoMedioPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    }
}