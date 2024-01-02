namespace SistemaGestaoVendas.Models.Produtos
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoMedio { get; set; }
        public double PrecoVenda { get; set; }
    }
}
