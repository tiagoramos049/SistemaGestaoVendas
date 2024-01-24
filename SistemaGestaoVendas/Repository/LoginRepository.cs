using Dapper;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Login;
using SistemaGestaoVendas.Models.Produtos;
using SistemaGestaoVendas.Models.Vendas;
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

        public bool ValidarLogin(Login login)
        {
            using (IDbConnection dbConnection = _dao.Connection) 
            {
                dbConnection.Open();
                string sql = $"select id from vendedor where email='{login.Email}' and senha='{login.Senha}'";
                return dbConnection.QueryFirstOrDefault<bool>(sql, login);
            }
        }
    }
}
