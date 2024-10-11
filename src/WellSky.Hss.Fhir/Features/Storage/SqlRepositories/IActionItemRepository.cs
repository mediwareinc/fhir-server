using WellSky.Hss.Fhir.Features.Storage.DataModels;

namespace WellSky.Hss.Fhir.Features.Storage.SqlRepositories
{
    public interface IActionItemRepository
    {
        Task AddAsync(string deploymentId, ActionItem journal);
        Task<ActionItem> GetAsync(string deploymentId, Guid id);
    }
}
