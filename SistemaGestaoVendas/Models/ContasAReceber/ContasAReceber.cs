namespace SistemaGestaoVendas.Models.ContasAReceber
{
    public class ContasAReceber
    {
        public int Id { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Favorecido { get; set; }
        public decimal Valor { get; set; }
        public string FormaPagamento { get; set; }
        public string Banco { get; set; }
    }
}
