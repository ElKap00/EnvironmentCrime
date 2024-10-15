using EnvironmentCrime.Models;

namespace EnvironmentCrime.Models
{
    public interface IEnvironmentCrimeRepository
    {
        //Read
        IQueryable<Errand> Errands { get; }
        IQueryable<Department> Departments { get; }
        IQueryable<ErrandStatus> ErrandStatuses { get; }
        IQueryable<Employee> Employees { get; }

        Task<Errand> GetErrandById(string errandId);

		//Create & Update
		bool SaveErrand(Errand errand);

		//Delete
		Errand DeleteErrand(string errandId);

	}
}
