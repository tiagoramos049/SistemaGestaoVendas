using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace SistemaGestaoVendas.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProduto _produtoRepository;
        public ProdutoController(IProduto produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public IActionResult Index()
        {
            var produto = _produtoRepository.GetAll();
            return View(produto);
        }
    }
}
