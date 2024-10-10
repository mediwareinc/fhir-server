namespace WellSky.Hss.Fhir.Features.Storage.InternalRepositories
{
    using System.Data;
    using Dapper;
    using DataModels;
    using Microsoft.Extensions.Logging;

    public partial class ActionItemRepository(
        ILogger<ActionItemRepository> logger,
        IDatabaseConnectionFactory databaseConnectionFactory)
        : BaseRepository(logger, databaseConnectionFactory), IActionItemRepository
    {
        public async Task AddAsync(string deploymentId, ActionItem actionItem)
        {
            using IDbConnection connection = await GetConnectionAsync(deploymentId);

            // TODO: Implement Insert logic
        }

        // Not planning to have a full implementation for now, only for testing connectivity with DBs
        public async Task<ActionItem> GetAsync(string deploymentId, Guid id)
        {
            using IDbConnection connection = await GetConnectionAsync(deploymentId);

            ActionItem journal = (await connection.QueryAsync<ActionItem>(GetActionItemSql, param: new { id })).FirstOrDefault();

            return journal;
        }
    }
}
