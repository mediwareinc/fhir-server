namespace WellSky.Hss.Fhir.Features.Storage.InternalRepositories
{
    using DataModels;

    public interface IActionItemRepository
    {
        Task AddAsync(string deploymentId, ActionItem journal);
        Task<ActionItem> GetAsync(string deploymentId, Guid id);
    }
}
