using EnvironmentCrime.Models;

namespace EnvironmentCrime.ViewModels
{
    public class StartCoordinatorViewModel
    {
        public IEnumerable<ErrandStatus>? ErrandStatuses { get; set; }
        public IEnumerable<Department>? Departments { get; set; }
        public IEnumerable<ErrandViewModel>? Errands { get; set; }
    }
}
