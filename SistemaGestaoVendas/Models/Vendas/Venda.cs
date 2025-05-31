namespace SistemaGestaoVendas.Models.Vendas
{
    public class Venda
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeCliente { get; set; } 
        public string Placa { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenda { get; set; }
        public DateTime DataVenda { get; set; }
    }
}
