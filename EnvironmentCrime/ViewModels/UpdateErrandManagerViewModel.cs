namespace EnvironmentCrime.ViewModels
{
    public class UpdateErrandManagerViewModel
    {
        public int ErrandID { get; set; }
        public string? EmployeeId { get; set; }
        public bool NoAction { get; set; }
        public string? Reason { get; set; }
    }
}
