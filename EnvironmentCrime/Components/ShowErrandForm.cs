using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Components
{
	public class ShowErrandForm : ViewComponent
	{
		private IEnvironmentCrimeRepository repository;

		public ShowErrandForm(IEnvironmentCrimeRepository repo)
		{
			repository = repo;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
