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
            bool loginOk = _login.ValidarLogin(login);

            if (!loginOk)
            {
                ModelState.AddModelError("LoginError", "Credenciais invalidas.");
                return View();
            }

            return Ok(loginOk);
        }
    }
}
