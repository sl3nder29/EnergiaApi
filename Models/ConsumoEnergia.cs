using System;

namespace EnergiaApi.Models
{
    public class ConsumoEnergia
    {
        public ConsumoEnergia()
        {
            Local = string.Empty;
            Equipamento = string.Empty;
        }

        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Local { get; set; }
        public double ConsumoKwH { get; set; }
        public string Equipamento { get; set; }
        public bool EstaAtivo { get; set; }
    }
} 