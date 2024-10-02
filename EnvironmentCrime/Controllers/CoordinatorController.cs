using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.ViewModels;

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
            return View();
        }

        public ViewResult ReportCrime()
        {
            return View();
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
            return View();
        }

		[HttpPost]
		public ViewResult Validate(Errand errand)
		{
			return View(errand);
		}
	}
}
