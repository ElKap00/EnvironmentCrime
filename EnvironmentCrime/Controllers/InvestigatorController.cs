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

		public ViewResult CrimeInvestigator(int id)
        {
			ViewBag.ID = id;
			return View(repository.ErrandStatuses);
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
