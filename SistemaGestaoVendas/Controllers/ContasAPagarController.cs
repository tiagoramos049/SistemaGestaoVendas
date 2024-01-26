using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.ContasAPagar;
using SistemaGestaoVendas.Models.Produtos;
using SistemaGestaoVendas.Repository;
using System.Drawing;

namespace SistemaGestaoVendas.Controllers
{
    public class ContasAPagarController : Controller
    {
        private readonly IContasAPagar _contasAPagarRepository;
        public ContasAPagarController(IContasAPagar contasAPagarRepository)
        {
            _contasAPagarRepository = contasAPagarRepository;
        }
        public IActionResult Index()
        {
            var contasAPagar = _contasAPagarRepository.GetAll();
            return View(contasAPagar);
        }

        [HttpPost]
        public IActionResult Index(ContasAPagar contasAPagar)
        {
            try
            {
                _contasAPagarRepository.Insert(contasAPagar);

                return RedirectToAction();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public IActionResult GridData(int page, int rows, string sidx, string sord)
        {
            var contasAPagars = _contasAPagarRepository.GetAll();

            contasAPagars = SortProdutos(contasAPagars, sidx, sord);

            var totalRecords = contasAPagars.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / rows);

            contasAPagars = contasAPagars.Skip((page - 1) * rows).Take(rows);

            var jsonData = contasAPagars.Select(p => new
            {
                id = p.Id,
                dataEmissao = p.DataEmissao,
                dataVencimento = p.DataVencimento,
                favorecido = p.Favorecido,
                valor = (decimal)p.Valor,
                formaPagamento = p.FormaPagamento,
                banco = p.Banco,
            });

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = jsonData
            });
        }

        private IEnumerable<ContasAPagar> SortProdutos(IEnumerable<ContasAPagar> contasAPagars, string sortBy, string sortOrder)
        {
            switch (sortBy)
            {
                case "nome":
                    contasAPagars = sortOrder == "asc" ? contasAPagars.OrderBy(p => p.Id) : contasAPagars.OrderByDescending(p => p.Id);
                    break;
            }

            return contasAPagars;
        }

        [HttpGet]
        public IActionResult GetDataForEdit(int id)
        {
            try
            {
                var contasAPagar = _contasAPagarRepository.GetById(id);

                if (contasAPagar == null)
                {
                    return Json(new { success = false, message = "Conta a Pagar não encontrada." });
                }

                return Json(new
                {
                    success = true,
                    id = contasAPagar.Id,
                    dataEmissao = contasAPagar.DataEmissao,
                    dataVencimento = contasAPagar.DataVencimento,
                    favorecido = contasAPagar.Favorecido,
                    valor = contasAPagar.Valor,
                    formaPagamento = contasAPagar.FormaPagamento,
                    banco = contasAPagar.Banco,

                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao obter dados para edição: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Update(int id, DateTime campo2, DateTime campo3, string campo4, decimal campo5, string campo6, string campo7)
        {
            try
            {
                var contasAPagar = _contasAPagarRepository.GetById(id);

                if (contasAPagar == null)
                {
                    return Json(new { success = false, message = "Contas a Pagar não encontrada." });
                }
                contasAPagar.Id = id;
                contasAPagar.DataEmissao = campo2;
                contasAPagar.DataVencimento = campo3;
                contasAPagar.Favorecido = campo4;
                contasAPagar.Valor = campo5;
                contasAPagar.FormaPagamento = campo6;
                contasAPagar.Banco = campo7;

                _contasAPagarRepository.Update(contasAPagar);

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
                _contasAPagarRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { success = true, message = "Erro ao Deletar o registro: " + ex.Message });
            }

        }
    }
}
