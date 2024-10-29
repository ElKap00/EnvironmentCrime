using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace EnvironmentCrime.Controllers
{
    [Authorize(Roles= "Investigator")]
    public class InvestigatorController : Controller
    {
		private readonly IEnvironmentCrimeRepository repository;
        private IWebHostEnvironment environment;
		private IHttpContextAccessor contextAcc;

		public InvestigatorController(IEnvironmentCrimeRepository repo, IWebHostEnvironment env, IHttpContextAccessor cont)
		{
			repository = repo;
            environment = env;
			contextAcc = cont;
		}

		public ViewResult CrimeInvestigator(int id)
        {
			var errand = repository.GetErrandById(id).Result;
            ViewBag.ErrandID = id;
            TempData["ID"] = id;
			ViewBag.ListOfStatuses = repository.ErrandStatuses;
			ViewBag.StatusID = errand.StatusId;

            return View(errand);
        }

        public ViewResult StartInvestigator()
        {
			var userName = contextAcc.HttpContext?.User?.Identity?.Name;
            userName = repository.Employees.FirstOrDefault(e => e.EmployeeId == userName)?.EmployeeName;
			var viewModel = new StartInvestigatorViewModel
			{
				ErrandStatuses = repository.ErrandStatuses,
				Errands = repository.GetAllErrands().Where(IHttpContextAccessor => IHttpContextAccessor.EmployeeName == userName)
			};
			return View(viewModel);
        }

		[HttpPost]
        public async Task<IActionResult> UpdateErrand(Errand model,string InvestigatorAction,string InvestigatorInfo, IFormFile loadSample, IFormFile loadImage)
		{
            int errandID = (int)TempData["ID"]!;
			var errand = repository.GetErrandById(errandID).Result;

            if (errand != null)
			{
				errand.InvestigatorInfo += "\n" + InvestigatorInfo;
				errand.InvestigatorAction +=  "\n" + InvestigatorAction;
                if (model.StatusId != "Välj")
                {
                    errand.StatusId = model.StatusId;
                }
				
                if (loadSample != null && loadSample.Length > 0)
                {
                    var sampleName = await SaveFile(loadSample, "ErrandSamples");
                    if (!string.IsNullOrEmpty(sampleName))
                    {
                        errand.Samples ??= new List<Sample>();
                        errand.Samples.Add(new Sample { SampleName = sampleName, ErrandID = errand.ErrandID });
                    }
                }

                if (loadImage != null && loadImage.Length > 0)
                {
                    var imageName = await SaveFile(loadImage, "ErrandImages");
                    if (!string.IsNullOrEmpty(imageName))
                    {
                        errand.Pictures ??= new List<Picture>();
                        errand.Pictures.Add(new Picture { PictureName = imageName, ErrandID = errand.ErrandID });
                    }
                }

                repository.SaveErrand(errand);
            }

            return RedirectToAction("CrimeInvestigator", new { id = errandID });
        }

        private async Task<string> SaveFile(IFormFile loadFile, string folderName)
        {
            if (loadFile == null || loadFile.Length == 0)
                return string.Empty;

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + loadFile.FileName;

            var filePath = Path.Combine(environment.WebRootPath, folderName, uniqueFileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await loadFile.CopyToAsync(stream);
			}

            return uniqueFileName;
        }
    }
}
