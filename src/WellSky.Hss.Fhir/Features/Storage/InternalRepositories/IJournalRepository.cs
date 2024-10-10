namespace WellSky.Hss.Fhir.Features.Storage.InternalRepositories
{
    using DataModels;

    public interface IJournalRepository
    {
        Task AddAsync(string deploymentId, Journal journal);
        Task<Journal> GetAsync(string deploymentId, Guid id);
    }
}
