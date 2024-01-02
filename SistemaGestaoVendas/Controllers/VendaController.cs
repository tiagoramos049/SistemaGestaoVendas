using Microsoft.AspNetCore.Mvc;

namespace SistemaGestaoVendas.Controllers
{
    public class VendasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
