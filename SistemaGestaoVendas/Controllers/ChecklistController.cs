using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Checklist;

namespace SistemaGestaoVendas.Controllers
{
    public class ChecklistController : Controller
    {
        private readonly ICheckList _checkListRepository;

        public ChecklistController(ICheckList checkListRepository)
        {
            _checkListRepository = checkListRepository;
        }

        public IActionResult Index(int idMotos)
        {
            var checklist = _checkListRepository.GetById(idMotos);
            if (checklist == null)
            {
                checklist = new CheckList
                {
                    IdMotos = idMotos,
                    ChecarDebitos = false,
                    ChecarAlinhamento = false,
                    ChecarCarenagem = false,
                    ChecarRodasPneus = false,
                    ChecarVazamentos = false,
                    ChecarManualChave = false,
                    ChecarPainel = false,
                    ChecarEletrica = false,
                    ChecarEmbreagem = false,
                    ChecarCaixaDirecao = false,
                    ChecarMotor = false,
                    ChecarFumacaDoMotor = false,
                    ChecarRolamentoDisco = false,
                    ChecarChassi = false
                };
            }

            ViewBag.IdMotos = idMotos;
            return View(checklist);
        }

        [HttpPost]
        public IActionResult Index(CheckList checklist)
        {
            if (ModelState.IsValid)
            {
                _checkListRepository.Update(checklist);
                // Redirecionar ou realizar alguma ação após salvar o checklist
                return RedirectToAction("Index", "Moto");
            }

            return View(checklist);
        }

        [HttpGet]
        public IActionResult List()
        {
            var checklists = _checkListRepository.GetAll();
            return View(checklists);
        }
    }
}