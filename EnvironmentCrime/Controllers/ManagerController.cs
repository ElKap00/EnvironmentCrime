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

		/// <summary>
		/// Prepares the errand, its status, and a list of employees who are part of the managers department for the view.
		/// </summary>
		/// <param name="id"></param>
		public ViewResult CrimeManager(int id)
        {
			var userDepartmentId = GetUserDepartmentId();
			var errand = repository.GetErrandById(id).Result;
            ViewBag.ErrandID = id;
            TempData["ID"] = id;
			ViewBag.StatusID = errand.StatusId;
            ViewBag.ListOfEmployees = repository.Employees.Where(emp => emp.DepartmentId == userDepartmentId);

            return View(errand);
        }

		/// <summary>
		/// Creates a new StartManagerViewModel including all errands assigned to the current managers department and sends it to the view.
		/// </summary>
		public ViewResult StartManager()
		{
			var userDepartmentId = GetUserDepartmentId();
			var userDepartmentName = GetUserDepartmentName();
			var viewModel = new StartManagerViewModel
			{
				ErrandStatuses = repository.ErrandStatuses,
				Employees = repository.Employees.Where(emp => emp.DepartmentId == userDepartmentId),
				Errands = repository.GetAllErrands().Where(errand => errand.DepartmentName == userDepartmentName)
			};
			return View(viewModel);
		}

		/// <summary>
		/// Filters the errands assigned to the current managers department based on the selected status, employee, or reference number.
		/// </summary>
		/// <param name="model"></param>
		[HttpPost]
		public IActionResult StartManager(StartManagerViewModel model)
		{
			var userDepartmentId = GetUserDepartmentId();
			var userDepartmentName = GetUserDepartmentName();

			var filteredErrands = repository.FilterErrands(null, userDepartmentName, null);

			if (!string.IsNullOrEmpty(model.RefNumber))
            {
                filteredErrands = repository.SearchByRefNumberAndDepartment(userDepartmentName!, model.RefNumber);
			}
            else
            {
				filteredErrands = repository.FilterErrands(model.SelectedStatus, userDepartmentName, model.SelectedEmployee);
			}

			if (filteredErrands.Count() == 0)
			{
				ViewBag.NoErrandsMessage = "Inga ärenden matchar din sökning";
			}

			model.ErrandStatuses = repository.ErrandStatuses.ToList();
			model.Employees = repository.Employees.Where(emp => emp.DepartmentId == userDepartmentId).ToList();
			model.Errands = filteredErrands;

			return View(model);
		}

		/// <summary>
		/// Updates the errand based on the managers input.
		/// </summary>
		/// <param name="model"></param>
		/// <param name="NoAction"></param>
		/// <param name="Reason"></param>
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

		// Helper methods

		private string GetUserDepartmentName()
		{
			var userName = contextAcc.HttpContext?.User?.Identity?.Name;
			var userDepartmentId = repository.Employees.FirstOrDefault(e => e.EmployeeId == userName)?.DepartmentId;
			return repository.Departments.FirstOrDefault(d => d.DepartmentId == userDepartmentId)?.DepartmentName;
		}

		private string GetUserDepartmentId()
		{
			var userName = contextAcc.HttpContext?.User?.Identity?.Name;
			return repository.Employees.FirstOrDefault(e => e.EmployeeId == userName)?.DepartmentId;
		}
	}
}
