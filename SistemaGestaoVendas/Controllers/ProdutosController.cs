using Microsoft.AspNetCore.Mvc;

namespace SistemaGestaoVendas.Controllers
{
    public class ProdutosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
