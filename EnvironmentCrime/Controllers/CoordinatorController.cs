using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.ViewModels;
using EnvironmentCrime.Infrastructure;

namespace EnvironmentCrime.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly IEnvironmentCrimeRepository repository;

        public CoordinatorController(IEnvironmentCrimeRepository repo)
        {
            repository = repo;
        }

        public ViewResult CrimeCoordinator(int id)
        {
            ViewBag.ID = id;
            return View(repository.Departments);
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
                Errands = repository.Errands
            };
            return View(viewModel);
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
	}
}
