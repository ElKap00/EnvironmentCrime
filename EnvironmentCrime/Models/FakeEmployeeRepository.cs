namespace EnvironmentCrime.Models
{
    public class FakeEmployeeRepository : IEmployeeRepository
    {
        public IQueryable<Employee> Employees => new List<Employee>
        {
            new Employee { EmployeeId = "E302", EmployeeName = "Martin Bäck", RoleTitle = "investigator", DepartmentId = "D01" },
            new Employee { EmployeeId = "E301", EmployeeName = "Lena Kristersson", RoleTitle = "investigator", DepartmentId = "D01" },
            new Employee { EmployeeId = "E401", EmployeeName = "Oskar Jansson", RoleTitle = "investigator", DepartmentId = "D02" },
            new Employee { EmployeeId = "E501", EmployeeName = "Susanne Strid", RoleTitle = "investigator", DepartmentId = "D03" },
        }.AsQueryable<Employee>();
    }
}
