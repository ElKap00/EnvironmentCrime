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

		/// <summary>
		/// Prepares the errand, its status and a list of statuses for the view.
		/// </summary>
		/// <param name="id"> The id of the chosen errand. </param>
		public ViewResult CrimeInvestigator(int id)
        {
			var errand = repository.GetErrandById(id).Result;
            ViewBag.ErrandID = id;
            TempData["ID"] = id;
			ViewBag.ListOfStatuses = repository.ErrandStatuses;
			ViewBag.StatusID = errand.StatusId;

            return View(errand);
        }

		/// <summary>
		/// Creates a new StartInvestigatorViewModel including all errands assigned to the current investigator and sends it to the view.
		/// </summary>
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

		/// <summary>
		/// Filters the errands assigned to the current investigator based on the selected status or reference number.
		/// </summary>
		/// <param name="model"></param>
		[HttpPost]
        public IActionResult StartInvestigator(StartInvestigatorViewModel model)
        {
			var userName = contextAcc.HttpContext?.User?.Identity?.Name;
			userName = repository.Employees.FirstOrDefault(e => e.EmployeeId == userName)?.EmployeeName;
			var filteredErrands = repository.FilterErrands(null, null, userName);

			if (!string.IsNullOrEmpty(model.RefNumber))
			{
				filteredErrands = repository.SearchByRefNumberAndEmployee(userName!, model.RefNumber);
			}
			else
			{
				if (!string.IsNullOrEmpty(model.SelectedStatus))
				{
					filteredErrands = repository.FilterErrands(model.SelectedStatus, null, userName);
				}
			}

			if (filteredErrands.Count() == 0)
			{
				ViewBag.NoErrandsMessage = "Inga ärenden matchar din sökning";
			}

			model.ErrandStatuses = repository.ErrandStatuses.ToList();
			model.Errands = filteredErrands;

			return View(model);
		}

		/// <summary>
		/// Updates the errand with the investigator's action and information and calls the methods to add sample and image.
		/// </summary>
		/// <param name="model"></param>
		/// <param name="InvestigatorAction"></param>
		/// <param name="InvestigatorInfo"></param>
		/// <param name="loadSample"></param>
		/// <param name="loadImage"></param>
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

				await AddSample(errand, loadSample);
				await AddImage(errand, loadImage);

				repository.SaveErrand(errand);
            }

            return RedirectToAction("CrimeInvestigator", new { id = errandID });
        }

		/// <summary>
		/// Adds an image to the errand if it exists.
		/// </summary>
		/// <param name="errand"></param>
		/// <param name="loadImage"></param>
		private async Task AddImage(Errand errand, IFormFile loadImage)
        {
			if (loadImage != null && loadImage.Length > 0)
			{
				var imageName = await SaveFile(loadImage, "ErrandImages");
				if (!string.IsNullOrEmpty(imageName))
				{
					errand.Pictures ??= new List<Picture>();
					errand.Pictures.Add(new Picture { PictureName = imageName, ErrandID = errand.ErrandID });
				}
			}
		}

		/// <summary>
		/// Adds a sample to the errand if it exists.
		/// </summary>
		/// <param name="errand"></param>
		/// <param name="loadSample"></param>
		private async Task AddSample(Errand errand, IFormFile loadSample)
		{
			if (loadSample != null && loadSample.Length > 0)
			{
				var sampleName = await SaveFile(loadSample, "ErrandSamples");
				if (!string.IsNullOrEmpty(sampleName))
				{
					errand.Samples ??= new List<Sample>();
					errand.Samples.Add(new Sample { SampleName = sampleName, ErrandID = errand.ErrandID });
				}
			}
		}

		/// <summary>
		/// Saves the file in the specified folder.
		/// </summary>
		/// <param name="loadFile"></param>
		/// <param name="folderName"></param>
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
