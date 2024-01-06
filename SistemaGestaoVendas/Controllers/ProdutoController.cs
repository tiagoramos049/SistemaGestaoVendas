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

            produtos = SortProdutos(produtos, sidx, sord);

            var totalRecords = produtos.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / rows);

            produtos = produtos.Skip((page - 1) * rows).Take(rows);

            var jsonData = produtos.Select(p => new
            {
                id = p.Id,
                nome = p.Nome,
                descricao = p.Descricao,
                preco_unitario = (decimal)p.Preco_Unitario,
                quantidade_estoque = (decimal)p.Quantidade_Estoque,
                unidade_medida = p.Unidade_Medida,
                link_foto = p.Link_Foto

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
            switch (sortBy)
            {
                case "nome":
                    produtos = sortOrder == "asc" ? produtos.OrderBy(p => p.Nome) : produtos.OrderByDescending(p => p.Nome);
                    break;
            }

            return produtos;
        }
        
        [HttpGet]
        public IActionResult GetDataForEdit(int id)
        {
            try
            {
                var produto = _produtoRepository.GetById(id);

                if (produto == null)
                {
                    return Json(new { success = false, message = "Produto não encontrado." });
                }

                return Json(new { success = true, campo1 = produto.Nome, campo2 = produto.Descricao, 
                               campo3 = produto.Preco_Unitario, campo4 = produto.Quantidade_Estoque, 
                                     campo5 = produto.Unidade_Medida, campo6 = produto.Link_Foto });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao obter dados para edição: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Update(int id, string campo1, string campo2, decimal campo3, int campo4, string campo5, string campo6)
        {
            try
            {
                var produto = _produtoRepository.GetById(id);

                if (produto == null)
                {
                    return Json(new { success = false, message = "Produto não encontrado." });
                }
                produto.Nome = campo1;
                produto.Descricao = campo2;
                produto.Preco_Unitario = campo3;
                produto.Quantidade_Estoque = campo4;
                produto.Unidade_Medida = campo5;
                produto.Link_Foto = campo6;
                
                _produtoRepository.Update(produto);

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
                _produtoRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { success = true, message = "Erro ao Deletar o registro: " + ex.Message });
            }
            
        }
    }
}
