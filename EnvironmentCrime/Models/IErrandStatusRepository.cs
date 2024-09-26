namespace EnvironmentCrime.Models
{
    public interface IErrandStatusRepository
    {
        IQueryable<ErrandStatus> ErrandStatuses { get; }
    }
}
