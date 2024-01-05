using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Vendedores;

namespace SistemaGestaoVendas.Controllers
{
    public class VendedorController : Controller
    {
        private readonly IVendedor _vendedorRepository;
        public VendedorController(IVendedor vendedor)
        {
            _vendedorRepository = vendedor;
        }

        public IActionResult Index()
        {
            var vendedor = _vendedorRepository.GetAll();
            return View(vendedor);
        }
        [HttpPost]
        public IActionResult Index(Vendedor vendedor)
        {
            try
            {
                _vendedorRepository.Insert(vendedor);

                return RedirectToAction();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult GridData(int page, int rows, string sidx, string sord)
        {
            var vendedores = _vendedorRepository.GetAll();

            // Lógica para ordenação
            vendedores = SortProdutos(vendedores, sidx, sord);

            // Lógica para paginação
            var totalRecords = vendedores.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / rows);

            // Aplica a paginação
            vendedores = vendedores.Skip((page - 1) * rows).Take(rows);

            var jsonData = vendedores.Select(p => new
            {
                id = p.Id,
                nome = p.Nome,
                email = p.Email,
                senha = p.Senha,
            });

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = jsonData
            });
        }

        private IEnumerable<Vendedor> SortProdutos(IEnumerable<Vendedor> vendedores, string sortBy, string sortOrder)
        {
            // Lógica para ordenação
            switch (sortBy)
            {
                case "nome":
                    vendedores = sortOrder == "asc" ? vendedores.OrderBy(p => p.Nome) : vendedores.OrderByDescending(p => p.Nome);
                    break;
                    // Adicione mais casos conforme necessário para outras colunas
            }

            return vendedores;
        }
    }
}
