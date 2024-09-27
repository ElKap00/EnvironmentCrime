using EnvironmentCrime.Models;

namespace EnvironmentCrime.Models
{
    public interface IEnvironmentCrimeRepository
    {
        IQueryable<Errand> Errands { get; }
        IQueryable<Department> Departments { get; }
        IQueryable<ErrandStatus> ErrandStatuses { get; }
        IQueryable<Employee> Employees { get; }

        Task<Errand> GetErrandById(string errandId);
	}
}
