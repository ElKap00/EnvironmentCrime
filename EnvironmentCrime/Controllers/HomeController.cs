using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

namespace EnvironmentCrime.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }
    }
}
