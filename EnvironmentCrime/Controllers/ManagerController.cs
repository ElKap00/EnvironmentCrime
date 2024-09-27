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

        public ViewResult CrimeManager()
        {
            return View();
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
    }
}
