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
		public IQueryable<Errand> Errands => context.Errands;
		public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;

		public Task<Errand> GetErrandById(string id)
		{
			return Task.Run(() => Errands.FirstOrDefault(e => e.RefNumber == id)!);
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
	}
}
