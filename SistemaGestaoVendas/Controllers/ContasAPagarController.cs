using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.ContasAPagarrs;
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
        [HttpPost]
        public IActionResult BaixarConta(int id)
        {
            try
            {
                var conta = _contasAPagarRepository.GetById(id);
                if (conta != null)
                {
                    conta.BaixarConta = true; // Marcar a conta como baixada
                    _contasAPagarRepository.Update(conta); // Atualizar a conta no banco de dados

                    // Retorna o ID da conta baixada e os dados atualizados da linha
                    return Json(new { success = true, message = "Conta a Pagar baixada com sucesso.", contasAPagarId = id, rowData = conta });
                }
                else
                {
                    return Json(new { success = false, message = "Conta não encontrada." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ReabrirConta(int id)
        {
            try
            {
                var conta = _contasAPagarRepository.GetById(id);
                if (conta != null)
                {
                    conta.BaixarConta = false; // Marcar a conta como aberta
                    _contasAPagarRepository.Update(conta); // Atualizar a conta no banco de dados

                    return Json(new { success = true, message = "Conta reaberta." });
                }
                else
                {
                    return Json(new { success = false, message = "Conta não encontrada." });
                }
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
                contaBaixada = p.BaixarConta,
                centroDeCusto = p.CentroDeCusto,
                categoria = p.Categoria,
                projeto = p.Projeto,
                numeroNotaFiscal = p.NumeroNotaFiscal,
                valorPagoNotaFiscal = p.ValorPagoNotaFiscal,
                jurosMulta = p.JurosMulta,
                desconto = p.Desconto,
                codigoBarra = p.CodigoBarra,
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
                    centroDeCusto = contasAPagar.CentroDeCusto,
                    categoria = contasAPagar.Categoria,
                    projeto = contasAPagar.Projeto,
                    numeroNotaFiscal = contasAPagar.NumeroNotaFiscal,
                    valorPagoNotaFiscal = contasAPagar.ValorPagoNotaFiscal,
                    jurosMulta = contasAPagar.JurosMulta,
                    desconto = contasAPagar.Desconto,
                    codigoBarra = contasAPagar.CodigoBarra,

                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao obter dados para edição: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Update(int id, DateTime campo2, DateTime campo3, string campo4, decimal campo5, string campo6, string campo7, string campo8, string campo9, string campo10, string campo11, decimal campo12, decimal campo13, decimal campo14, string campo15)
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
                contasAPagar.CentroDeCusto = campo8;
                contasAPagar.Categoria = campo9;
                contasAPagar.Projeto = campo10;
                contasAPagar.NumeroNotaFiscal = campo11;
                contasAPagar.ValorPagoNotaFiscal = campo12;
                contasAPagar.JurosMulta = campo13;
                contasAPagar.Desconto = campo14;
                contasAPagar.CodigoBarra = campo15;

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
