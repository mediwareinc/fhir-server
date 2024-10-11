namespace WellSky.Hss.Fhir.Features.Storage.SqlRepositories
{
    using DataModels;

    public interface IActionItemRepository
    {
        Task AddAsync(string deploymentId, ActionItem journal);
    }
}
