using SistemaGestaoVendas.Models.Clientes;

namespace SistemaGestaoVendas.Models.Motos
{
    public class Moto
    {
        public int IdMotos { get; set; }
        public string Nome { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public DateTime Fabricacao { get; set; }
        public string Crv { get; set; }
        public string Cor { get; set; }
        public string Chassi { get; set; }
        public string Cilindrada { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCompra { get; set; }
        public DateTime DataVenda { get; set; }
        public DateTime DataCompra { get; set; }
        public string Observacoes { get; set; }
        public string Regiao { get; set; }
        public int IdCliente { get; set; }

        // Propriedade de Navegação
        public Cliente Cliente { get; set; }
        public bool Estoque {  get; set; }
    }
}
