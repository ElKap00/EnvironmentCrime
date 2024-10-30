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

		//Search by reference number
		IQueryable<ErrandViewModel> SearchByRefNumber(string refNumber);

		//Filter
		IQueryable<ErrandViewModel> FilterErrands(
			string? statusName,
			string? departmentName,
			string? employeeName);

		//Search errands by reference number and employee
		IQueryable<ErrandViewModel> SearchByRefNumberAndEmployee(string employeeName, string refNumber);

		//Search errands by reference number and department
		IQueryable<ErrandViewModel> SearchByRefNumberAndDepartment(string departmentName, string refNumber);
	}
}
