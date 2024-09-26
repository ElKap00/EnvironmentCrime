namespace EnvironmentCrime.Models
{
    public class FakeErrandStatusRepository : IErrandStatusRepository
    {
        public IQueryable<ErrandStatus> ErrandStatuses => new List<ErrandStatus>
        {
            new ErrandStatus { StatusId = "S_A", StatusName = "Rapporterad" },
            new ErrandStatus { StatusId = "S_B", StatusName = "Ingen åtgärd" },
            new ErrandStatus { StatusId = "S_C", StatusName = "Startad" },
            new ErrandStatus { StatusId = "S_D", StatusName = "Färdig" },
        }.AsQueryable<ErrandStatus>();
    }
}
