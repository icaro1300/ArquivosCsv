using System.ComponentModel;

namespace csv.Models
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int QuantidadeDisponivel { get; set; }
    }
}
