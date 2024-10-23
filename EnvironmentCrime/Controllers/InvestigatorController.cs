using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace EnvironmentCrime.Controllers
{
    public class InvestigatorController : Controller
    {
		private readonly IEnvironmentCrimeRepository repository;
        private IWebHostEnvironment environment;

        public InvestigatorController(IEnvironmentCrimeRepository repo, IWebHostEnvironment env)
		{
			repository = repo;
            environment = env;
        }

		public ViewResult CrimeInvestigator(int id)
        {
			var errand = repository.GetErrandById(id).Result;
            ViewBag.ErrandID = id;
			ViewBag.ListOfStatuses = repository.ErrandStatuses;
			ViewBag.StatusID = errand.StatusId;

            return View(errand);
        }

        public ViewResult StartInvestigator()
        {
			var viewModel = new StartInvestigatorViewModel
			{
				ErrandStatuses = repository.ErrandStatuses,
				Errands = repository.Errands
			};
			return View(viewModel);
        }

		[HttpPost]
        public async Task<IActionResult> UpdateErrand(Errand model,string InvestigatorAction,string InvestigatorInfo, IFormFile loadSample, IFormFile loadImage)
		{
			var errand = repository.GetErrandById(model.ErrandID).Result;

            if (errand != null)
			{
				errand.InvestigatorInfo += "\n" + InvestigatorInfo;
				errand.InvestigatorAction +=  "\n" + InvestigatorAction;
                errand.StatusId = model.StatusId;

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

            return RedirectToAction("CrimeInvestigator", new { id = model.ErrandID });
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
