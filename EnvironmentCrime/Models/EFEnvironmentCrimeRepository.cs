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
	}
}
