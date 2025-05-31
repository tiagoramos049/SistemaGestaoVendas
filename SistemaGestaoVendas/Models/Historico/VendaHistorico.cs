namespace SistemaGestaoVendas.Models.Historico
{
    public class VendaHistorico
    {
        public int IdMotos { get; set; }
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string NomeCliente { get; set; }
        public string Placa { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenda { get; set; }
        public DateTime DataVenda { get; set; }
    }
}
