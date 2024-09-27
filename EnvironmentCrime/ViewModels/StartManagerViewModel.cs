using EnvironmentCrime.Models;

namespace EnvironmentCrime.ViewModels
{
    public class StartManagerViewModel
    {
        public IEnumerable<ErrandStatus> ErrandStatuses { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Errand> Errands { get; set; }
    }
}
