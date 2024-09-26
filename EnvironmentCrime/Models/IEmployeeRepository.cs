namespace EnvironmentCrime.Models
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Employees { get; }
    }
}
