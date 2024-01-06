namespace SistemaGestaoVendas.Models.Clientes
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf_Cnpj { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }
}
