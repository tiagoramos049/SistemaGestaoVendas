namespace SistemaGestaoVendas.Models.ContasAReceberrs
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
        public bool BaixarConta { get; set; }
    }
}
