using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using System.Threading.Tasks;

namespace EnvironmentCrime.Components
{
	public class ShowOneErrand : ViewComponent
	{
		private IEnvironmentCrimeRepository repository;

		public ShowOneErrand(IEnvironmentCrimeRepository repo)
		{
			repository = repo;
		}

		public async Task<IViewComponentResult> InvokeAsync(string id)
		{
			var errand = await repository.GetErrandById(id);
			return View(errand);
		}
	}
}
