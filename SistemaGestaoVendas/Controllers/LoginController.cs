using Dapper;
using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Login;
using SistemaGestaoVendas.Models.Vendedores;
using System.Data;

namespace SistemaGestaoVendas.Controllers
{
    public class LoginController : Controller
    {
        private readonly Dao _dao;
        public LoginController(Dao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Login login)
        {
            var cliente = GetClienteByEmailAndPassword(login.Email, login.Senha);
            var vendedor = GetVendedorByEmailAndPassword(login.Email, login.Senha);

            if (cliente != null)
            {
                // Usuário autenticado como cliente
                ViewBag.UsuarioLogado = "Cliente";
            }
            else if (vendedor != null)
            {
                // Usuário autenticado como vendedor
                ViewBag.UsuarioLogado = "Vendedor";
            }
            else
            {
                // Falha na autenticação, redirecione ou retorne uma mensagem de erro
                ViewBag.UsuarioLogado = "Não Logado";
            }

            return View();
        }
        private async Task<Cliente> GetClienteByEmailAndPassword(string email, string senha)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return await dbConnection.QueryFirstOrDefaultAsync<Cliente>("SELECT * FROM Cliente WHERE email = @email AND senha = @senha",
                     new { Email = email, Senha = senha });
            }
        }

        private Vendedor GetVendedorByEmailAndPassword(string email, string senha)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Vendedor>("SELECT * FROM Vendedor WHERE email = @email AND senha = @senha",
                    new { Email = email, Senha = senha });
            }
        }
    }
}
