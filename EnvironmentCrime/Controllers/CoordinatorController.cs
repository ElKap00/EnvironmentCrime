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

        public ViewResult CrimeCoordinator()
        {
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

        public ViewResult Validate()
        {
            return View();
        }
    }
}
