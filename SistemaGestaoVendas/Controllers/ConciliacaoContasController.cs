using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.AutoMapper;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models;
using SistemaGestaoVendas.Models.ContasAPagarrs;
using SistemaGestaoVendas.Models.ContasAReceberrs;

namespace SistemaGestaoVendas.Controllers
{
    public class ConciliacaoContasController : Controller
    {
        private readonly IContasAPagar _contasAPagarRepository;
        private readonly IContasAReceber _contasAReceberRepository;

        public ConciliacaoContasController(IContasAPagar contasAPagar, IContasAReceber contasAReceber)
        {
            _contasAPagarRepository = contasAPagar;
            _contasAReceberRepository = contasAReceber;
        }

        public IActionResult ConciliacaoContas()
        {
            var contasAPagar = _contasAPagarRepository.GetAll();
            var contasAReceber = _contasAReceberRepository.GetAll();

            var viewModel = new ConciliacaoViewModel
            {
                ContasAPagar = contasAPagar,
                ContasAReceber = contasAReceber
            };

            return View(viewModel);
        }
    }
}
