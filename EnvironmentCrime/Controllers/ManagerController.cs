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
            ViewBag.ID = id;
			return View(repository.Employees);
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
