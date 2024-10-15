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
			if (errand.RefNumber == null)
			{
				context.Errands.Add(errand);
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
			return errand;
		}
	}
}
