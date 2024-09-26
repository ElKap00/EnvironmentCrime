using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

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
            return View(repository);
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
