namespace WellSky.Hss.Fhir.Features.Storage.InternalRepositories
{
    using DataModels;

    public interface IConsumerRepository
    {
        Task AddAsync(string deploymentId, Consumer provider);
        Task<Consumer> GetAsync(string deploymentId, Guid id);
    }
}
