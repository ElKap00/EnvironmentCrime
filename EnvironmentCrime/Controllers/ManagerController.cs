using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace EnvironmentCrime.Controllers
{
	[Authorize(Roles = "Manager")]
	public class ManagerController : Controller
    {
        private readonly IEnvironmentCrimeRepository repository;
        private IHttpContextAccessor contextAcc;

		public ManagerController(IEnvironmentCrimeRepository repo, IHttpContextAccessor cont)
        {
            repository = repo;
			contextAcc = cont;
		}

        public ViewResult CrimeManager(int id)
        {
            var errand = repository.GetErrandById(id).Result;
            ViewBag.ErrandID = id;
            TempData["ID"] = id;
			ViewBag.StatusID = errand.StatusId;
            ViewBag.ListOfEmployees = repository.Employees;

            return View(errand);
        }

		public ViewResult StartManager()
		{
			var userName = contextAcc.HttpContext?.User?.Identity?.Name;
			var userDepartmentId = repository.Employees.FirstOrDefault(e => e.EmployeeId == userName)?.DepartmentId;
            var userDepartmentName = repository.Departments.FirstOrDefault(d => d.DepartmentId == userDepartmentId)?.DepartmentName;
			var viewModel = new StartManagerViewModel
			{
				ErrandStatuses = repository.ErrandStatuses,
				Employees = repository.Employees.Where(emp => emp.DepartmentId == userDepartmentId),
				Errands = repository.GetAllErrands().Where(errand => errand.DepartmentName == userDepartmentName)
			};
			return View(viewModel);
		}

        [HttpPost]
        public IActionResult UpdateErrand(Errand model, bool NoAction, string Reason)
		{
            int errandID = (int)TempData["ID"]!;
			var errand = repository.GetErrandById(errandID).Result;
            if (errand != null)
            {
                if (NoAction)
                {
                    errand.StatusId = "S_B";
                    errand.EmployeeId = null;
                    errand.InvestigatorInfo = Reason ?? "Ingen kommentar";
                }
                else
                {
                    errand.StatusId = "S_A";
                    errand.EmployeeId = model.EmployeeId;
                }
                repository.SaveErrand(errand);
            }
            return RedirectToAction("CrimeManager", new { id = errandID });
        }
	}
}
