using Dapper;
using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Login;
using SistemaGestaoVendas.Models.Vendas;
using SistemaGestaoVendas.Models.Vendedores;
using System.Data;
using System.Diagnostics.Eventing.Reader;

namespace SistemaGestaoVendas.Controllers
{
    public class LoginController : Controller
    {
        private readonly Dao _dao;
        private readonly ILogin _login;
        public LoginController(Dao dao, ILogin login)
        {
            _dao = dao; _login = login;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login login)
        {
            var validarEmailSenhaCliente = _login.ValidarEmailSenhaCliente(login.Email, login.Senha);
            var validarEmailSenhaVendedor = _login.ValidarEmailSenhaVendedor(login.Email, login.Senha);

            if (validarEmailSenhaCliente != null && validarEmailSenhaCliente.Email == login.Email && validarEmailSenhaCliente.Senha == login.Senha)
            {
                return RedirectToAction("Index", "Produto", new { userType = "Cliente" });
            }
            else if (validarEmailSenhaVendedor != null && validarEmailSenhaVendedor.Email == login.Email && validarEmailSenhaVendedor.Senha == login.Senha)
            {
                return RedirectToAction("Index", "Produto", new { userType = "Vendedor" });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
