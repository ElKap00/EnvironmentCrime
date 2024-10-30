using EnvironmentCrime.Models;

namespace EnvironmentCrime.ViewModels
{
    public class StartManagerViewModel
    {
        public IEnumerable<ErrandStatus>? ErrandStatuses { get; set; }
        public IEnumerable<Employee>? Employees { get; set; }
        public IEnumerable<ErrandViewModel>? Errands { get; set; }

		// Filter criteria
		public string? SelectedStatus { get; set; }
		public string? SelectedEmployee { get; set; }
		public string? RefNumber { get; set; }
	}
}
