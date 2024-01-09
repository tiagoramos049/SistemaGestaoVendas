using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Login;
using SistemaGestaoVendas.Models.Produtos;
using SistemaGestaoVendas.Models.Vendedores;

namespace SistemaGestaoVendas.Interfaces
{
    public interface ILogin
    {
        public Vendedor ValidarEmailSenhaVendedor(string email, string senha);
        public Cliente ValidarEmailSenhaCliente(string email, string senha); 
    }
}
