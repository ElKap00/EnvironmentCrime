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


		/// <summary>
		/// Saves the errand in the database and shows the reference number.
		/// Removes the errand from the session.
		/// </summary>
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

		/// <summary>
		/// Validates and saves the errand in the session.
		/// </summary>
		/// <param name="errand">The errand to validate and save.</param>
		[HttpPost]
		public ViewResult Validate(Errand errand) 
        {
            HttpContext.Session.Set<Errand>("CitizenErrand", errand);
			return View(errand);
        }
    }
}
