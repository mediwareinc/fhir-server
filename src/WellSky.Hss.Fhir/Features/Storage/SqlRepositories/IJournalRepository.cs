namespace WellSky.Hss.Fhir.Features.Storage.SqlRepositories
{
    using DataModels;

    public interface IJournalRepository
    {
        Task AddAsync(string deploymentId, Journal journal);
    }
}
