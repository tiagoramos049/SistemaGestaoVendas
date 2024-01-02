using Microsoft.AspNetCore.Mvc;

namespace SistemaGestaoVendas.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
