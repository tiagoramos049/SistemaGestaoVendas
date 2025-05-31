namespace SistemaGestaoVendas.Models.Clientes
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CpfCnpj { get; set; }
        public string Observacoes { get; set; }
    }
}
