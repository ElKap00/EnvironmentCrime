using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.ViewModels;

namespace EnvironmentCrime.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IEnvironmentCrimeRepository repository;

        public ManagerController(IEnvironmentCrimeRepository repo)
        {
            repository = repo;
        }

        public ViewResult CrimeManager(int id)
        {
            var errand = repository.GetErrandById(id).Result;
            ViewBag.ErrandID = id;
            ViewBag.StatusID = errand.StatusId;
            ViewBag.ListOfEmployees = repository.Employees;
            var viewModel = new UpdateErrandManagerViewModel
            {
                ErrandID = errand.ErrandID,
                EmployeeId = errand.EmployeeId,
                Reason = errand.InvestigatorInfo ?? string.Empty
            };
            return View(viewModel);
        }

        public ViewResult StartManager()
        {
            var viewModel = new StartManagerViewModel
            {
                ErrandStatuses = repository.ErrandStatuses,
                Employees = repository.Employees,
                Errands = repository.Errands
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateErrand(UpdateErrandManagerViewModel model)
		{
            var errand = repository.GetErrandById(model.ErrandID).Result;
            if (errand != null)
            {
                if (model.NoAction)
                {
                    errand.StatusId = "S_B";
                    errand.EmployeeId = null;
                    errand.InvestigatorInfo = model.Reason ?? "Ingen kommentar";
                }
                else
                {
                    errand.StatusId = "S_A";
                    errand.EmployeeId = model.EmployeeId;
                    errand.InvestigatorInfo = null;
                }
                repository.SaveErrand(errand);
            }
            return RedirectToAction("CrimeManager", new { id = model.ErrandID });
        }
	}
}
