﻿using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.ContasAPagarrs;
using SistemaGestaoVendas.Models.ContasAReceberrs;

namespace SistemaGestaoVendas.Controllers
{
    public class ContasAReceberController : Controller
    {
        private readonly IContasAReceber _contasAReceberRepository;
        public ContasAReceberController(IContasAReceber contasAReceberRepository)
        {
            _contasAReceberRepository = contasAReceberRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var contasAReceber = _contasAReceberRepository.GetAll();
            return View(contasAReceber);
        }

        [HttpGet]
        public IActionResult DetalhesConta(int id)
        {
            try
            {
                var conta = _contasAReceberRepository.GetById(id);
                if (conta == null)
                {
                    return Json(new { success = false, message = "Conta não encontrada." });
                }

                return Json(new ContasAReceber
                {
                    Success = conta.Success,
                    DataEmissao = conta.DataEmissao,
                    DataVencimento = conta.DataVencimento,
                    Favorecido = conta.Favorecido,
                    Valor = conta.Valor,
                    FormaPagamento = conta.FormaPagamento,
                    Banco = conta.Banco,
                    CentroDeCusto = conta.CentroDeCusto,
                    Categoria = conta.Categoria,
                    Projeto = conta.Projeto,
                    NumeroNotaFiscal = conta.NumeroNotaFiscal,
                    ValorPagoNotaFiscal = conta.ValorPagoNotaFiscal,
                    JurosMulta = conta.JurosMulta,
                    Desconto = conta.Desconto,
                    CodigoBarra = conta.CodigoBarra
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
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
        [HttpPost]
        public IActionResult BaixarConta(int id)
        {
            try
            {
                var conta = _contasAReceberRepository.GetById(id);
                if (conta != null)
                {
                    conta.BaixarConta = true; 
                    _contasAReceberRepository.Update(conta);

                    return Json(new { success = true, message = "Conta baixada com sucesso." });
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
                var conta = _contasAReceberRepository.GetById(id);
                if (conta != null)
                {
                    conta.BaixarConta = false; 
                    _contasAReceberRepository.Update(conta);

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
            var contasAReceber = _contasAReceberRepository.GetAll();

            contasAReceber = SortProdutos(contasAReceber, sidx, sord);

            var totalRecords = contasAReceber.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / rows);

            contasAReceber = contasAReceber.Skip((page - 1) * rows).Take(rows);

            var jsonData = contasAReceber.Select(p => new
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
                    banco = contasAReceber.Banco,
                    centroDeCusto = contasAReceber.CentroDeCusto,
                    categoria = contasAReceber.Categoria,
                    projeto = contasAReceber.Projeto,
                    numeroNotaFiscal = contasAReceber.NumeroNotaFiscal,
                    valorPagoNotaFiscal = contasAReceber.ValorPagoNotaFiscal,
                    jurosMulta = contasAReceber.JurosMulta,
                    desconto = contasAReceber.Desconto,
                    codigoBarra = contasAReceber.CodigoBarra,

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
                contasAReceber.CentroDeCusto = campo8;
                contasAReceber.Categoria = campo9;
                contasAReceber.Projeto = campo10;
                contasAReceber.NumeroNotaFiscal = campo11;
                contasAReceber.ValorPagoNotaFiscal = campo12;
                contasAReceber.JurosMulta = campo13;
                contasAReceber.Desconto = campo14;
                contasAReceber.CodigoBarra = campo15;

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
