using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace EnvironmentCrime.Controllers
{
	public class HomeController : Controller
	{
		private UserManager<IdentityUser> userManager;
		private SignInManager<IdentityUser> signInManager;

		public HomeController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
		{
			userManager = userMgr;
			signInManager = signInMgr;
		}

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

		public ViewResult Login(string returnUrl)
		{
			return View(new LoginModel
			{
				ReturnUrl = returnUrl
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel loginModel)
		{
			if (ModelState.IsValid)
			{
				IdentityUser user = await userManager.FindByNameAsync(loginModel.UserName);
				if (user != null)
				{
					await signInManager.SignOutAsync();
					if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
					{
						if (await userManager.IsInRoleAsync(user, "Coordinator"))
						{
							return Redirect(loginModel.ReturnUrl ?? "/Coordinator/StartCoordinator");
						}
						else if (await userManager.IsInRoleAsync(user, "Investigator"))
						{
							return Redirect(loginModel.ReturnUrl ?? "/Investigator/StartInvestigator");
						}
						else if (await userManager.IsInRoleAsync(user, "Manager"))
						{
							return Redirect(loginModel.ReturnUrl ?? "/Manager/StartManager");
						}
						else
						{
							ModelState.AddModelError("", "Användare har ingen giltig roll.");
						}
					}
					else
					{
						ModelState.AddModelError("", "Ogiltig login försök.");
					}
				}
				else
				{
					ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord");
				}
			}
			
			return View(loginModel);
		}

		public async Task<RedirectResult> Logout(string returnUrl = "/")
		{
			await signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}
	}
}
