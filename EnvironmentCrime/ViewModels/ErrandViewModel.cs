namespace EnvironmentCrime.ViewModels
{
	public class ErrandViewModel
	{
		public DateTime DateOfObservation { get; set; }
		public int ErrandID { get; set; }
		public string? RefNumber { get; set; }
		public string? TypeOfCrime { get; set; }
		public string? StatusName { get; set; }
		public string? DepartmentName { get; set; }
		public string? EmployeeName { get; set; }
	}
}
