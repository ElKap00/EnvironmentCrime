using Microsoft.AspNetCore.Mvc;

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
