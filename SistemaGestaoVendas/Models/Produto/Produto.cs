namespace SistemaGestaoVendas.Models.Produtos
{
    public class Produto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco_Unitario { get; set; }
        public decimal Quantidade_Estoque { get; set; }
        public string? Unidade_Medida { get; set; }
        public string? Link_Foto { get; set; }
        public List<Produto>? produtosImportados { get; set; }
    }
}
