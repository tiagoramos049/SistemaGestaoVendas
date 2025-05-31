using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Login;

namespace SistemaGestaoVendas.Interfaces
{
    public interface ILogin
    {
        public bool ValidarLogin(Login login);
    }
}
