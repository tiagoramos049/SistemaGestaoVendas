using SistemaGestaoVendas.Models.Produtos;

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
        public string CentroDeCusto { get; set; }
        public string Categoria { get; set; }
        public string Projeto { get; set; }
        public string NumeroNotaFiscal { get; set; }
        public decimal ValorPagoNotaFiscal { get; set; }
        public decimal JurosMulta { get; set; }
        public decimal Desconto { get; set; }
        public string CodigoBarra { get; set; }
    }
}
