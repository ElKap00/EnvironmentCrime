using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.Infrastructure;

namespace EnvironmentCrime.Controllers
{
    public class CitizenController : Controller
    {
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
			//TODO: Save the errand in the database
			HttpContext.Session.Remove("NewErrand");
			return View();
        }

        [HttpPost]
		public ViewResult Validate(Errand errand) 
        {
            HttpContext.Session.Set<Errand>("NewErrand", errand);
			return View(errand);
        }
    }
}
