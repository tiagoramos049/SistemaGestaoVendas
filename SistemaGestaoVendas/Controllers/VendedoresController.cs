using Microsoft.AspNetCore.Mvc;

namespace SistemaGestaoVendas.Controllers
{
    public class VendedoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
