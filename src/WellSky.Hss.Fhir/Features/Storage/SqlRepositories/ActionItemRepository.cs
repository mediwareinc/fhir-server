using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using WellSky.Hss.Fhir.Features.Storage.DataModels;

namespace WellSky.Hss.Fhir.Features.Storage.SqlRepositories
{
    public partial class ActionItemRepository(
        ILogger<ActionItemRepository> logger,
        IDatabaseConnectionFactory databaseConnectionFactory)
        : BaseRepository(logger, databaseConnectionFactory), IActionItemRepository
    {
        public async Task AddAsync(string deploymentId, ActionItem actionItem)
        {
            using IDbConnection connection = await GetConnectionAsync(deploymentId);

            // TODO: Remove this once actual INSERT implementation is done, this is only for testing connectivity with DBs
            var testId = "690A198C-5B26-4644-B4FB-768B4C00F3B0";
            ActionItem dataFromDb = (await connection.QueryAsync<ActionItem>(GetActionItemSql, param: new { id = testId })).FirstOrDefault();

            // TODO: Implement Insert logic
        }

        public Task<ActionItem> GetAsync(string deploymentId, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
