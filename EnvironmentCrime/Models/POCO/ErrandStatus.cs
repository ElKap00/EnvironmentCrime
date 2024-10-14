using System.ComponentModel.DataAnnotations;

namespace EnvironmentCrime.Models
{
    public class ErrandStatus
    {
        [Key]
        public required string StatusId { get; set; }
        public required string StatusName { get; set; }
    }
}
