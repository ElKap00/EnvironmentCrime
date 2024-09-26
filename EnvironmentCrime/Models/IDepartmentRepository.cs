namespace EnvironmentCrime.Models
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> Departments { get; }
    }
}
