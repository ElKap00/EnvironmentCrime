using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using System.Reflection;
using EnvironmentCrime.Infrastructure;

namespace EnvironmentCrime.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            var errand = HttpContext.Session.Get<Errand>("CitizenErrand");
			if (errand == null)
			{
				return View();
			}
			else
			{
				return View(errand);
			}
        }

        public ViewResult Login()
        {
            return View();
        }
    }
}
