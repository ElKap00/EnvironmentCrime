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

        public ViewResult CrimeCoordinator(string id)
        {
            ViewBag.ID = id;
            return View(repository.Departments);
        }

        public ViewResult ReportCrime()
        {
            var errand = HttpContext.Session.Get<Errand>("NewErrand");
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
			var errand = HttpContext.Session.Get<Errand>("NewErrand");
			if (errand != null)
			{
				repository.SaveErrand(errand);
			}
			HttpContext.Session.Remove("NewErrand");
			return View(errand);
        }

		[HttpPost]
		public ViewResult Validate(Errand errand)
		{
			HttpContext.Session.Set<Errand>("NewErrand", errand);
			return View(errand);
		}
	}
}
