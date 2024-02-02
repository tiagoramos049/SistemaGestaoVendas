namespace SistemaGestaoVendas.Models.ContasAPagarrs
{
    public class ContasAPagar
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
