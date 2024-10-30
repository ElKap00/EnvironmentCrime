using EnvironmentCrime.Models;

namespace EnvironmentCrime.ViewModels
{
	public class StartInvestigatorViewModel
	{
		public IEnumerable<ErrandStatus>? ErrandStatuses { get; set; }
		public IEnumerable<ErrandViewModel>? Errands { get; set; }

		// Filter criteria
		public string? SelectedStatus { get; set; }
		public string? RefNumber { get; set; }
	}
}
