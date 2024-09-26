namespace EnvironmentCrime.Models
{
    public interface IErrandRepository
    {
        IQueryable<Errand> Errands { get; }
    }
}
