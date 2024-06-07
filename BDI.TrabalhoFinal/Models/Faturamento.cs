namespace BDI.TrabalhoFinal.Models
{
    public class Faturamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public string? TipoPagamento { get; set; }
    }
}
