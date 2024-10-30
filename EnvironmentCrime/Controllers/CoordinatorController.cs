using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.ViewModels;
using EnvironmentCrime.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace EnvironmentCrime.Controllers
{
	[Authorize(Roles = "Coordinator")]
	public class CoordinatorController : Controller
    {
        private readonly IEnvironmentCrimeRepository repository;

		public CoordinatorController(IEnvironmentCrimeRepository repo)
        {
            repository = repo;
		}

		public ViewResult CrimeCoordinator(int id)
        {
            var errand = repository.GetErrandById(id).Result;
			ViewBag.ID = id;
			TempData["ID"] = id;
			ViewBag.ListOfDepartments = repository.Departments.Skip(1);
			return View(errand);
        }

        public ViewResult ReportCrime()
        {
            var errand = HttpContext.Session.Get<Errand>("CoordinatorErrand");
			if (errand == null)
			{
				return View();
			}
			else
			{
				return View(errand);
			}
		}

        public ViewResult StartCoordinator()
        {
			var viewModel = new StartCoordinatorViewModel
			{
				ErrandStatuses = repository.ErrandStatuses,
				Departments = repository.Departments,
				Errands = repository.GetAllErrands()
			};
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult StartCoordinator(StartCoordinatorViewModel model)
		{
			var filteredErrands = repository.GetAllErrands();

			if (!string.IsNullOrEmpty(model.RefNumber))
			{
				filteredErrands = filteredErrands.Where(e => e.RefNumber.Contains(model.RefNumber));
			}
			else
			{
				if (!string.IsNullOrEmpty(model.SelectedStatus))
				{
					filteredErrands = filteredErrands.Where(e => e.StatusName == model.SelectedStatus);
				}
				if (!string.IsNullOrEmpty(model.SelectedDepartment))
				{
					filteredErrands = filteredErrands.Where(e => e.DepartmentName == model.SelectedDepartment);
				}
			}

			if (filteredErrands.Count() == 0)
            {
                ViewBag.NoErrandsMessage = "Inga ärenden matchar din sökning";
            }

            model.ErrandStatuses = repository.ErrandStatuses.ToList();
			model.Departments = repository.Departments.ToList();
			model.Errands = filteredErrands;

			return View(model);
		}


		public ViewResult Thanks()
        {
			var errand = HttpContext.Session.Get<Errand>("CoordinatorErrand");
			if (errand != null)
			{
				repository.SaveErrand(errand);
                ViewBag.RefNumber = errand.RefNumber;
			}
			HttpContext.Session.Remove("CoordinatorErrand");
			return View();
        }

		[HttpPost]
		public ViewResult Validate(Errand errand)
		{
			HttpContext.Session.Set<Errand>("CoordinatorErrand", errand);
			return View(errand);
		}

        public IActionResult UpdateDepartment(string departmentId)
		{
			int errandID = (int)TempData["ID"]!;
			var errand = repository.GetErrandById(errandID).Result;
			if (errand != null && departmentId != "Välj")
			{
				errand.DepartmentId = departmentId;
				repository.SaveErrand(errand);
			}

			return RedirectToAction("CrimeCoordinator", new { id = errandID });
		}
	}
}
