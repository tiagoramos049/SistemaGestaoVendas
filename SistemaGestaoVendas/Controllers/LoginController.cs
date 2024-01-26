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
        public IActionResult Index(int? id)
        {
            if (id !=null) 
            {
                if (id == 0) 
                {
                    HttpContext.Session.SetString("NomeUsuarioLogado", string.Empty);
                    HttpContext.Session.SetString("SenhaUsuarioLogado", string.Empty);
                }
            }

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
            else 
            {
                HttpContext.Session.SetString("NomeUsuarioLogado", login.Email);
                HttpContext.Session.SetString("SenhaUsuarioLogado", login.Senha);
                return RedirectToAction("Index","Produto");
            }
        }
    }
}
