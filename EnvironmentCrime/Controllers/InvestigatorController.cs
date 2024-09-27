using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.ViewModels;

namespace EnvironmentCrime.Controllers
{
    public class InvestigatorController : Controller
    {
		private readonly IEnvironmentCrimeRepository repository;

		public InvestigatorController(IEnvironmentCrimeRepository repo)
		{
			repository = repo;
		}

		public ViewResult CrimeInvestigator(string id)
        {
			ViewBag.ID = id;
			return View();
        }

        public ViewResult StartInvestigator()
        {
			var viewModel = new StartInvestigatorViewModel
			{
				ErrandStatuses = repository.ErrandStatuses,
				Errands = repository.Errands
			};
			return View(viewModel);
        }
    }
}
