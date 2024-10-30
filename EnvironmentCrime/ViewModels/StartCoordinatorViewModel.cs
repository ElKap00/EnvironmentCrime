using EnvironmentCrime.Models;

namespace EnvironmentCrime.ViewModels
{
    public class StartCoordinatorViewModel
    {
        public IEnumerable<ErrandStatus>? ErrandStatuses { get; set; }
        public IEnumerable<Department>? Departments { get; set; }
        public IEnumerable<ErrandViewModel>? Errands { get; set; }

		// Filter criteria
		public string? SelectedStatus { get; set; }
		public string? SelectedDepartment { get; set; }
		public string? RefNumber { get; set; }
	}
}
