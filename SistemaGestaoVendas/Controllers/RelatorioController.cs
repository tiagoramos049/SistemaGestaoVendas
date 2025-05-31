using Dapper;
using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Historico;
using SistemaGestaoVendas.Models.Motos;
using SistemaGestaoVendas.Models.Relatorio;
using SistemaGestaoVendas.Models.Vendas;
using System.Data;
using System.Globalization;

namespace SistemaGestaoVendas.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly IMoto _moto;
        private readonly Dao _dao;

        public RelatorioController(IMoto moto, Dao dao)
        {
            _moto = moto;
            _dao = dao;

        }
        public ActionResult Index()
        {
            var culture = new CultureInfo("pt-BR"); // Cultura brasileira
            var motos = _moto.GetAll();
            var vendas = _moto.GetAllVendas();
            var totalMotos = motos.Count(x => x.Estoque);
            var totalValorCompra = motos.Sum(m => m.ValorCompra);
            var totalValorVenda = motos.Sum(m => m.ValorVenda);
            var totalMargemLucro = motos.Sum(m => m.ValorVenda - m.ValorCompra);
            var historicoVendas = vendas.Select(v => new VendaHistorico
            {
                IdMotos = v.Id,
                Nome = v.Nome,
                NomeCliente = v.NomeCliente,
                Placa = v.Placa,
                ValorCompra = v.ValorCompra,
                ValorVenda = v.ValorVenda,
                DataVenda = v.DataVenda
            }).OrderBy(v => v.DataVenda).ToList();
            var valorTotalVendas = historicoVendas.Sum(v => v.ValorVenda);
            var relatorio = new RelatorioViewModel
            {
                TotalMotos = totalMotos,
                TotalMargemLucro = totalMargemLucro,
                TotalValorCompra = totalValorCompra,
                TotalValorVenda = totalValorVenda,
                Vendas = motos.Select(m => new Venda
                {
                    Id = m.IdMotos,
                    Nome = m.Nome,
                    Placa = m.Placa,
                    ValorCompra = m.ValorCompra,
                    ValorVenda = m.ValorVenda
                }).ToList(),
                Estoque = motos.Where(m => m.Estoque).ToList(),
                HistoricoVendas = historicoVendas,
                ValorTotalVendas = valorTotalVendas
            };
            return View(relatorio);
        }
    }
}
