using System;

namespace EnergiaApi.Models
{
    public class RelatorioDiario
    {
        public DateTime Data { get; set; }
        public double ConsumoTotal { get; set; }
        public double ConsumoMedio { get; set; }
        public double ConsumoMaximo { get; set; }
        public double ConsumoMinimo { get; set; }
        public int QuantidadeAlertas { get; set; }
    }
} 