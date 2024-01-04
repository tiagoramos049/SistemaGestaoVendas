using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using SistemaGestaoVendas.Models.Produtos;

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

        [HttpPost]
        public IActionResult Index(Produto produto)
        {
            try
            {
                _produtoRepository.Insert(produto);

                // Retorna o produto recém-inserido junto com o sucesso
                return RedirectToAction();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public IActionResult GridData(int page, int rows, string sidx, string sord)
        {
            var produtos = _produtoRepository.GetAll();

            // Lógica para ordenação
            produtos = SortProdutos(produtos, sidx, sord);

            // Lógica para paginação
            var totalRecords = produtos.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / rows);

            // Aplica a paginação
            produtos = produtos.Skip((page - 1) * rows).Take(rows);

            var jsonData = produtos.Select(p => new
            {
                id = p.Id,
                nome = p.Nome,
                descricao = p.Descricao,
                preco_unitario = (decimal)p.Preco_Unitario
            });

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = jsonData
            });
        }

        private IEnumerable<Produto> SortProdutos(IEnumerable<Produto> produtos, string sortBy, string sortOrder)
        {
            // Lógica para ordenação
            switch (sortBy)
            {
                case "nome":
                    produtos = sortOrder == "asc" ? produtos.OrderBy(p => p.Nome) : produtos.OrderByDescending(p => p.Nome);
                    break;
                    // Adicione mais casos conforme necessário para outras colunas
            }

            return produtos;
        }
    }
}
