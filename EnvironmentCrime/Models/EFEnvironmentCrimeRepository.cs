using EnvironmentCrime.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EnvironmentCrime.Models
{
	public class EFEnvironmentCrimeRepository : IEnvironmentCrimeRepository
	{
		private readonly ApplicationDbContext context;
		public EFEnvironmentCrimeRepository(ApplicationDbContext ctx)
		{
			context = ctx;
		}

		public IQueryable<Department> Departments => context.Departments;
		public IQueryable<Employee> Employees => context.Employees;
		public IQueryable<Errand> Errands => context.Errands.Include(e => e.Pictures).Include(e => e.Samples);
		public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;

		public Task<Errand> GetErrandById(int id)
		{
			return Task.Run(() => Errands.FirstOrDefault(e => e.ErrandID == id)!);
		}

		public bool SaveErrand(Errand errand)
		{
			if (errand.ErrandID == 0)
			{
				var sequence = context.Sequences.FirstOrDefault(s => s.Id == 1);

				errand.RefNumber = GetNextRefNumber(sequence!);
				errand.StatusId = "S_A";

				context.Errands.Add(errand);

				IncreaseSequence(sequence!);
			}
			else
			{
				context.Errands.Update(errand);
			}
			return context.SaveChanges() >= 0;
		}

		public Errand DeleteErrand(string id)
		{
			Errand errand = context.Errands.FirstOrDefault(e => e.RefNumber == id)!;
			if (errand != null)
			{
				context.Errands.Remove(errand);
				context.SaveChanges();
			}
			return errand!;
		}

		private string GetNextRefNumber(Sequence sequence)
		{
			int currentYear = DateTime.Now.Year;
			string referenceNumber = $"{currentYear}-45-{sequence!.CurrentValue}";
			return referenceNumber;
		}

		private void IncreaseSequence(Sequence sequence)
		{
			sequence!.CurrentValue++;
			context.Sequences.Update(sequence);
			context.SaveChanges();
		}

		public IQueryable<ErrandViewModel> GetAllErrands()
		{
			var errandList = from err in context.Errands
							 join stat in context.ErrandStatuses on err.StatusId equals stat.StatusId
							 join dep in context.Departments on err.DepartmentId equals dep.DepartmentId
								into depErrand from dep in depErrand.DefaultIfEmpty()
							 join emp in context.Employees on err.EmployeeId equals emp.EmployeeId
								into empErrand from emp in empErrand.DefaultIfEmpty()
							 orderby err.RefNumber descending

							 select new ErrandViewModel
							 {
								 DateOfObservation = err.DateOfObservation,
								 ErrandID = err.ErrandID,
								 RefNumber = err.RefNumber,
								 TypeOfCrime = err.TypeOfCrime,
								 StatusName = stat.StatusName,
								 DepartmentName = (err.DepartmentId == null ? "ej tillsatt" : dep.DepartmentName),
								 EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : emp.EmployeeName)
							 };

			return errandList;
		}
	}
}
