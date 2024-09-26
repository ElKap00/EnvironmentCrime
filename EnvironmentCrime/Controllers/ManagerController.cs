using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

namespace EnvironmentCrime.Controllers
{
    public class ManagerController : Controller
    {
        public ViewResult CrimeManager()
        {
            return View();
        }

        public ViewResult StartManager()
        {
            return View();
        }
    }
}
