using Microsoft.AspNetCore.Mvc;

namespace SistemaGestaoVendas.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
