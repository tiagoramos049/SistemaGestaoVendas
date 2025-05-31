using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
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
                id = p.IdCliente,
                nome = p.Nome,
                telefone = p.Telefone,
                cpfCnpj = p.CpfCnpj,
                observacoes = p.Observacoes,
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

        [HttpGet]
        public IActionResult GetDataForEdit(int id)
        {
            try
            {
                var cliente = _clienteRepository.GetById(id);

                if (cliente == null)
                {
                    return Json(new { success = false, message = "Produto não encontrado." });
                }

                return Json(new
                {
                    success = true,
                    campo1 = cliente.Nome,
                    campo2 = cliente.Telefone,
                    campo3 = cliente.CpfCnpj,
                    campo4 = cliente.Observacoes,
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao obter dados para edição: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Update(int id, string campo1, string campo2, string campo3, string campo4)
        {
            try
            {
                var cliente = _clienteRepository.GetById(id);

                if (cliente == null) 
                {
                    return Json(new { success = false, message = "Cliente não encontrado." });
                }

                cliente.Nome = campo1;
                cliente.Telefone = campo2;
                cliente.CpfCnpj = campo3;
                cliente.Observacoes = campo4;

                _clienteRepository.Update(cliente);

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
