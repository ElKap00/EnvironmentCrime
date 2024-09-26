using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

namespace EnvironmentCrime.Controllers
{
    public class InvestigatorController : Controller
    {
        public ViewResult CrimeInvestigator()
        {
            return View();
        }

        public ViewResult StartInvestigator()
        {
            return View();
        }
    }
}
