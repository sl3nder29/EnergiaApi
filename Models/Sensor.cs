namespace EnergiaApi.Models
{
    public class Sensor
    {
        public Sensor()
        {
            Nome = string.Empty;
            Local = string.Empty;
            Tipo = string.Empty;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public bool EstaAtivo { get; set; }
        public double ConsumoAtual { get; set; }
        public string Tipo { get; set; }
    }
} 