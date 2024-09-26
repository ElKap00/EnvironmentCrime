namespace EnvironmentCrime.Models
{
    public class FakeDepartmentRepository : IDepartmentRepository
    {
        public IQueryable<Department> Departments => new List<Department>
        {
            new Department { DepartmentId = "D00", DepartmentName = "Småstads kommun" },
            new Department { DepartmentId = "D01", DepartmentName = "IT-avdelningen" },
            new Department { DepartmentId = "D02", DepartmentName = "Lek och Skoj" },
            new Department { DepartmentId = "D03", DepartmentName = "Miljöskydd" },
        }.AsQueryable<Department>();
    }
}
