using System;

namespace EnergiaApi.Models
{
    public class Alerta
    {
        public Alerta()
        {
            Mensagem = string.Empty;
            Local = string.Empty;
        }

        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Mensagem { get; set; }
        public string Local { get; set; }
        public double ConsumoAtual { get; set; }
        public double Limite { get; set; }
        public bool EstaAtivo { get; set; }
    }
} 