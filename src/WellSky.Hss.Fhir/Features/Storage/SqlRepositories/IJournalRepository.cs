using WellSky.Hss.Fhir.Features.Storage.DataModels;

namespace WellSky.Hss.Fhir.Features.Storage.SqlRepositories
{
    public interface IJournalRepository
    {
        Task AddAsync(string deploymentId, Journal journal);
        Task<Journal> GetAsync(string deploymentId, Guid id);
    }
}
