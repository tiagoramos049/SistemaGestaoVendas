using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Login;
using SistemaGestaoVendas.Models.Produtos;
using SistemaGestaoVendas.Models.Vendedores;

namespace SistemaGestaoVendas.Interfaces
{
    public interface ILogin
    {
        public bool ValidarLogin(Login login);
    }
}
