using SistemaGestaoVendas.Models.Historico;
using SistemaGestaoVendas.Models.Motos;
using SistemaGestaoVendas.Models.Vendas;

namespace SistemaGestaoVendas.Models.Relatorio
{
    public class RelatorioViewModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int TotalMotos { get; set; }
        public decimal TotalMargemLucro { get; set; }
        public decimal TotalValorCompra { get; set; }
        public decimal TotalValorVenda { get; set; }
        public List<Venda> Vendas { get; set; }
        public List<Moto> Estoque { get; set; }
        public List<VendaHistorico> HistoricoVendas { get; set; }
        public decimal ValorTotalVendas { get; set; }
    }
}
