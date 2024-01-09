using Dapper;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Login;
using SistemaGestaoVendas.Models.Produtos;
using SistemaGestaoVendas.Models.Vendedores;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SistemaGestaoVendas.Repository
{
    public class LoginRepository : ILogin
    {
        private readonly Dao _dao;
        public LoginRepository(Dao dao)
        {
            _dao = dao;
        }
        public Cliente ValidarEmailSenhaCliente(string? email, string? senha)
        {
            using (IDbConnection? dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Cliente>("SELECT ID FROM CLIENTE WHERE EMAIL = @Email AND SENHA = @Senha", new { Email = email, Senha = senha });
            }
        }

        public Vendedor ValidarEmailSenhaVendedor(string? email, string? senha)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Vendedor>("SELECT ID FROM VENDEDOR WHERE EMAIL = @Email AND SENHA = @Senha", new { Email = email, Senha = senha });
            }
        }
    }
}
