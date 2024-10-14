using EnvironmentCrime.Models;

namespace EnvironmentCrime.ViewModels
{
	public class StartInvestigatorViewModel
	{
		public IEnumerable<ErrandStatus>? ErrandStatuses { get; set; }
		public IEnumerable<Errand>? Errands { get; set; }
	}
}
