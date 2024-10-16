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
			var errand = HttpContext.Session.Get<Errand>("CitizenErrand");
			if (errand != null)
			{
				repository.SaveErrand(errand);
                ViewBag.RefNumber = errand.RefNumber;
			}
			HttpContext.Session.Remove("CitizenErrand");
			return View();
        }

        [HttpPost]
		public ViewResult Validate(Errand errand) 
        {
            HttpContext.Session.Set<Errand>("CitizenErrand", errand);
			return View(errand);
        }
    }
}
