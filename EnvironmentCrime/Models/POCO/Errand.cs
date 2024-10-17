using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EnvironmentCrime.Models
{
    public class Errand
    {
		public int ErrandID { get; set; }
		public required string RefNumber { get; set; }

		[Required(ErrorMessage = "Ange brottets plats")]
		[StringLength(100, ErrorMessage = "Platsen får inte vara längre än 100 tecken")]
		[Display(Name = "Var har brottet skett någonstans?")]
		public required string Place { get; set; }

		[Required(ErrorMessage = "Ange brottets typ")]
		[StringLength(50, ErrorMessage = "Typen av brott får inte vara längre än 50 tecken")]
		[Display(Name = "Vilken typ av brott?")]
		public required string TypeOfCrime { get; set; }

		[Required(ErrorMessage = "Ange datum för brottet")]
		[DataType(DataType.Date)]
		[Display(Name = "När skedde brottet?")]
		public DateTime DateOfObservation { get; set; }

		[Required(ErrorMessage = "Ange namn")]
		[StringLength(50, ErrorMessage = "Namnet får inte vara längre än 50 tecken")]
		[Display(Name = "Ditt namn (för- och efternamn)")]
		public required string InformerName { get; set; }

		[Phone]
		[Required(ErrorMessage = "Ange telefonnummer")]
		[RegularExpression(@"^(\d{3}-\d{7})$", ErrorMessage = "Ange telefonnummer i formatet 070-1234567")]
		[Display(Name = "Din telefon")]
		public required string InformerPhone { get; set; }

		[StringLength(500, ErrorMessage = "Observationen får inte vara längre än 500 tecken")]
		[Display(Name = "Beskriv din observation (ex. namn på misstänkt person)")]
		public string? Observation { get; set; }
		public string? InvestigatorInfo { get; set; }
        public string? InvestigatorAction { get; set; }
        public string? StatusId { get; set; }
        public string? DepartmentId { get; set; }
        public string? EmployeeId { get; set; }
		public ICollection<Sample>? Samples { get; set; }
		public ICollection<Picture>? Pictures { get; set; }
	}
}
