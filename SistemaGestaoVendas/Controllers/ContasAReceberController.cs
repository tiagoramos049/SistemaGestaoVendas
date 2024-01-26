using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.ContasAPagar;
using SistemaGestaoVendas.Models.ContasAReceber;

namespace SistemaGestaoVendas.Controllers
{
    public class ContasAReceberController : Controller
    {
        private readonly IContasAReceber _contasAReceberRepository;
        public ContasAReceberController(IContasAReceber contasAReceberRepository)
        {
            _contasAReceberRepository = contasAReceberRepository;
        }
        public IActionResult Index()
        {
            var contasAReceber = _contasAReceberRepository.GetAll();
            return View(contasAReceber);
        }

        [HttpPost]
        public IActionResult Index(ContasAReceber contasAReceber)
        {
            try
            {
                _contasAReceberRepository.Insert(contasAReceber);

                return RedirectToAction();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public IActionResult GridData(int page, int rows, string sidx, string sord)
        {
            var contasARecebers = _contasAReceberRepository.GetAll();

            contasARecebers = SortProdutos(contasARecebers, sidx, sord);

            var totalRecords = contasARecebers.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / rows);

            contasARecebers = contasARecebers.Skip((page - 1) * rows).Take(rows);

            var jsonData = contasARecebers.Select(p => new
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

        private IEnumerable<ContasAReceber> SortProdutos(IEnumerable<ContasAReceber> contasARecebers, string sortBy, string sortOrder)
        {
            switch (sortBy)
            {
                case "nome":
                    contasARecebers = sortOrder == "asc" ? contasARecebers.OrderBy(p => p.Id) : contasARecebers.OrderByDescending(p => p.Id);
                    break;
            }

            return contasARecebers;
        }

        [HttpGet]
        public IActionResult GetDataForEdit(int id)
        {
            try
            {
                var contasAReceber = _contasAReceberRepository.GetById(id);

                if (contasAReceber == null)
                {
                    return Json(new { success = false, message = "Conta a Receber não encontrada." });
                }

                return Json(new
                {
                    success = true,
                    id = contasAReceber.Id,
                    dataEmissao = contasAReceber.DataEmissao,
                    dataVencimento = contasAReceber.DataVencimento,
                    favorecido = contasAReceber.Favorecido,
                    valor = contasAReceber.Valor,
                    formaPagamento = contasAReceber.FormaPagamento,
                    banco = contasAReceber.Banco

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
                var contasAReceber = _contasAReceberRepository.GetById(id);

                if (contasAReceber == null)
                {
                    return Json(new { success = false, message = "Contas a Receber não encontrada." });
                }
                contasAReceber.Id = id;
                contasAReceber.DataEmissao = campo2;
                contasAReceber.DataVencimento = campo3;
                contasAReceber.Favorecido = campo4;
                contasAReceber.Valor = campo5;
                contasAReceber.FormaPagamento = campo6;
                contasAReceber.Banco = campo7;

                _contasAReceberRepository.Update(contasAReceber);

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
                _contasAReceberRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { success = true, message = "Erro ao Deletar o registro: " + ex.Message });
            }

        }
    }
}
