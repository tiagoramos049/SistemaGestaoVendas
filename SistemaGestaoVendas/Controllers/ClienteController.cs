using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Produtos;
using SistemaGestaoVendas.Repository;

namespace SistemaGestaoVendas.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ICliente _clienteRepository;
        public ClienteController(ICliente cliente)
        {
            _clienteRepository = cliente;
        }

        public IActionResult Index()
        {
            var cliente = _clienteRepository.GetAll();
            return View(cliente);
        }
        [HttpPost]
        public IActionResult Index(Cliente cliente)
        {
            try
            {
                _clienteRepository.Insert(cliente);

                return RedirectToAction();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult GridData(int page, int rows, string sidx, string sord)
        {
            var clientes = _clienteRepository.GetAll();

            // Lógica para ordenação
            clientes = SortProdutos(clientes, sidx, sord);

            // Lógica para paginação
            var totalRecords = clientes.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / rows);

            // Aplica a paginação
            clientes = clientes.Skip((page - 1) * rows).Take(rows);

            var jsonData = clientes.Select(p => new
            {
                id = p.Id,
                nome = p.Nome,
                cpf_cnpj = p.Cpf_Cnpj,
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

        private IEnumerable<Cliente> SortProdutos(IEnumerable<Cliente> clientes, string sortBy, string sortOrder)
        {
            // Lógica para ordenação
            switch (sortBy)
            {
                case "nome":
                    clientes = sortOrder == "asc" ? clientes.OrderBy(p => p.Nome) : clientes.OrderByDescending(p => p.Nome);
                    break;
                    // Adicione mais casos conforme necessário para outras colunas
            }

            return clientes;
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var produto = _clienteRepository.GetById(id);
                await _clienteRepository.Update(produto);

                return RedirectToAction("Index"); // Substitua "Index" pela sua ação desejada
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
                _clienteRepository.Delete(id);

                return RedirectToAction("Index"); // Substitua "Index" pela sua ação desejada
            }
            catch (Exception ex)
            {
                return Json(new { success = true, message = "Erro ao Deletar o registro: " + ex.Message });
            }

        }

    }

}
