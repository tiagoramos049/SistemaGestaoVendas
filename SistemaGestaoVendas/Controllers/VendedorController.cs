using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Vendedores;
using SistemaGestaoVendas.Repository;

namespace SistemaGestaoVendas.Controllers
{
    public class VendedorController : Controller
    {
        private readonly IVendedor _vendedorRepository;
        public VendedorController(IVendedor vendedor)
        {
            _vendedorRepository = vendedor;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var vendedor = _vendedorRepository.GetAll();
                return View(vendedor);
            }
            catch (Exception ex)
            {
                return View("Error",ex);
            }
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

        [HttpGet]
        public IActionResult GetDataForEdit(int id)
        {
            try
            {
                var vendedor = _vendedorRepository.GetById(id);

                if (vendedor == null)
                {
                    return Json(new { success = false, message = "Vendedor não encontrado." });
                }

                return Json(new
                {
                    success = true,
                    campo1 = vendedor.Nome,
                    campo2 = vendedor.Email,
                    campo3 = vendedor.Senha,
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao obter dados para edição: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Update(int id, string campo1, string campo2, string campo3)
        {
            try
            {
                var vendedor = _vendedorRepository.GetById(id);

                if (vendedor == null)
                {
                    return Json(new { success = false, message = "Vendedor não encontrado." });
                }
                vendedor.Nome = campo1;
                vendedor.Email = campo2;
                vendedor.Senha = campo3;

                _vendedorRepository.Update(vendedor);

                return Json(new { success = true, message = "Registro atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao atualizar o registro: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _vendedorRepository.Delete(id);
                return RedirectToAction("Index"); // Substitua "Index" pela sua ação desejada
            }
            catch (Exception ex)
            {
                return Json(new { success = true, message = "Erro ao Deletar o registro: " + ex.Message });
            }
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
