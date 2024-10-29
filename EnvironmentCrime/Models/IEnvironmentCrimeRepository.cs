using EnvironmentCrime.Models;
using EnvironmentCrime.ViewModels;

namespace EnvironmentCrime.Models
{
    public interface IEnvironmentCrimeRepository
    {
        //Read
        IQueryable<Errand> Errands { get; }
        IQueryable<Department> Departments { get; }
        IQueryable<ErrandStatus> ErrandStatuses { get; }
        IQueryable<Employee> Employees { get; }

        Task<Errand> GetErrandById(int errandId);

		//Create & Update
		bool SaveErrand(Errand errand);

		//Delete
		Errand DeleteErrand(string errandId);

		//Get all errands
		IQueryable<ErrandViewModel> GetAllErrands();
	}
}
