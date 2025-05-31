using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Historico;
using SistemaGestaoVendas.Models.Motos;
using SistemaGestaoVendas.Repository;

namespace SistemaGestaoVendas.Controllers
{
    public class MotoController : Controller
    {
        private readonly IMoto _vendedorMoto;
        private readonly Dao _dao;
        public MotoController(Dao dao, IMoto moto)
        {
            _dao = dao; _vendedorMoto = moto;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var moto = _vendedorMoto.GetAll();
                return View(moto);
            }
            catch (Exception ex)
            {
                return View("Error",ex);
            }
        }
        [HttpPost]
        public IActionResult Insert(Moto moto)
        {
            try
            {
                int motoId = _vendedorMoto.Insert(moto);
                return Ok(new { success = true, idMoto = motoId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        public IActionResult GridData(int page, int rows, string sidx, string sord)
        {
            var motos = _vendedorMoto.GetAll();

            // Lógica para ordenação
            motos = SortProdutos(motos, sidx, sord);

            // Lógica para paginação
            var totalRecords = motos.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / rows);

            // Aplica a paginação
            motos = motos.Skip((page - 1) * rows).Take(rows);

            var jsonData = motos.Select(p => new
            {
                id = p.IdMotos,
                nome = p.Nome,
                placa = p.Placa,
                marca = p.Marca,
                fabricacao = p.Fabricacao,
                crv = p.Crv,
                cor = p.Cor,
                chassi = p.Chassi,
                cilindrada = p.Cilindrada,
                valorVenda = p.ValorVenda,
                valorCompra = p.ValorCompra,
                dataVenda = p.DataVenda,
                dataCompra = p.DataCompra,
                observacoes = p.Observacoes,
                regiao = p.Regiao,
                idCliente = p.IdCliente,
                estoque = p.Estoque,
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
                var vendedor = _vendedorMoto.GetById(id);

                if (vendedor == null)
                {
                    return Json(new { success = false, message = "Moto não encontrado." });
                }

                return Json(new
                {
                    success = true,
                    campo1 = vendedor.Nome,
                    campo2 = vendedor.Placa,
                    campo3 = vendedor.Marca,
                    campo4 = vendedor.Fabricacao,
                    campo5 = vendedor.Crv,
                    campo6 = vendedor.Cor,
                    campo7 = vendedor.Chassi,
                    campo8 = vendedor.Cilindrada,
                    campo9 = vendedor.ValorVenda,
                    campo10 = vendedor.ValorCompra,
                    campo11 = vendedor.DataVenda,
                    campo12 = vendedor.DataCompra,
                    campo13 = vendedor.Observacoes,
                    campo14 = vendedor.Regiao,
                    campo15 = vendedor.IdCliente,
                    campo16 = vendedor.Estoque,
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao obter dados para edição: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Update(int id, string campo1, string campo2, string campo3, DateTime campo4, string campo5, string campo6, string campo7, string campo8, decimal campo9, decimal campo10, DateTime campo11, DateTime campo12, string campo13, string campo14)
        {
            try
            {
                var vendedor = _vendedorMoto.GetById(id);

                if (vendedor == null)
                {
                    return Json(new { success = false, message = "Vendedor não encontrado." });
                }
                vendedor.Nome = campo1;
                vendedor.Placa = campo2;
                vendedor.Marca = campo3;
                vendedor.Fabricacao = campo4;
                vendedor.Crv = campo5;
                vendedor.Cor = campo6;
                vendedor.Chassi = campo7;
                vendedor.Cilindrada = campo8;
                vendedor.ValorVenda = campo9;
                vendedor.ValorCompra = campo10;
                
                vendedor.DataCompra = campo12;
                vendedor.Observacoes = campo13;
                vendedor.Regiao = campo14;
                vendedor.IdCliente = 1;
                vendedor.Estoque = true;

                _vendedorMoto.Update(vendedor);

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
                _vendedorMoto.Delete(id);
                return RedirectToAction("Index"); // Substitua "Index" pela sua ação desejada
            }
            catch (Exception ex)
            {
                return Json(new { success = true, message = "Erro ao Deletar o registro: " + ex.Message });
            }
        }
        private IEnumerable<Moto> SortProdutos(IEnumerable<Moto> vendedores, string sortBy, string sortOrder)
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
        [HttpPost]
        public ActionResult RegistrarVenda(int idMoto, int idCliente)
        {
            try
            {
                _vendedorMoto.AtualizarEstoqueERegistrarVenda(idMoto, idCliente);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        [Route("Moto/RegistrarVenda")]
        public IActionResult RegistrarVenda([FromBody] VendaHistorico vendaData)
        {
            try
            {
                // Exibir dados recebidos para depuração
                Console.WriteLine($"IdMoto: {vendaData.IdMotos}, IdCliente: {vendaData.IdCliente}");

                _vendedorMoto.AtualizarEstoqueERegistrarVenda(vendaData.IdMotos, vendaData.IdCliente);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult ObterClientes()
        {
            try
            {
                var clientes = _vendedorMoto.ObterTodosClientes();
                return Json(new { success = true, clientes = clientes });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
