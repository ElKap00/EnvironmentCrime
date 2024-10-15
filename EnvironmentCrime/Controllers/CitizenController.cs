using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.Infrastructure;

namespace EnvironmentCrime.Controllers
{
    public class CitizenController : Controller
    {
		private readonly IEnvironmentCrimeRepository repository;

		public CitizenController(IEnvironmentCrimeRepository repo)
		{
			repository = repo;
		}

		public ViewResult Contact()
        {
            return View();
        }

        public ViewResult Faq()
        {
            return View();
        }

        public ViewResult Services()
        {
            return View();
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
