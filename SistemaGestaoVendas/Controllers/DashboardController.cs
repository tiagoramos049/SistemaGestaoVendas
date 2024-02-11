using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Repository;

namespace SistemaGestaoVendas.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IContasAPagar _contasAPagarRepository;
        private readonly IContasAReceber _contasAReceberRepository;
        private readonly IConciliacaoContas _conciliacaoContasRepository;
        private readonly DashboardRepository _dashboardRepository;
        private readonly Dao _dao;

        public DashboardController(IContasAPagar contasAPagar, IContasAReceber contasAReceber, IConciliacaoContas conciliacaoContasRepository, Dao dao, DashboardRepository dashBoardRepository)
        {
            _contasAPagarRepository = contasAPagar;
            _contasAReceberRepository = contasAReceber;
            _conciliacaoContasRepository = conciliacaoContasRepository ?? throw new ArgumentNullException(nameof(conciliacaoContasRepository));
            _dao = dao;
            _dashboardRepository = dashBoardRepository;
        }
        public IActionResult Index()
        {
            // Obter valores das contas a pagar e contas a receber do repositório
            var valorContasAPagar = _contasAPagarRepository.GetAll();
            var valorContasAReceber = _contasAReceberRepository.GetAll();

            // Passar os valores para a view usando ViewBag
            ViewBag.ValorContasAPagar = valorContasAPagar;
            ViewBag.ValorContasAReceber = valorContasAReceber;

            return View();
        }
        public IActionResult Dashboard()
        {
            var totalContasAPagar = _dashboardRepository.GetTotalContasAPagar();
            var totalContasAReceber = _dashboardRepository.GetTotalContasAReceber();

            ViewBag.ValorContasAPagar = totalContasAPagar;
            ViewBag.ValorContasAReceber = totalContasAReceber;

            return View();
        }
    }
}
