using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using WellSky.Hss.Fhir.Features.Storage.DataModels;

namespace WellSky.Hss.Fhir.Features.Storage.SqlRepositories
{
    public partial class JournalRepository(
        ILogger<JournalRepository> logger,
        IDatabaseConnectionFactory databaseConnectionFactory)
        : BaseRepository(logger, databaseConnectionFactory), IJournalRepository
    {
        public async Task AddAsync(string deploymentId, Journal journal)
        {
            using IDbConnection connection = await GetConnectionAsync(deploymentId);

            // TODO: Remove this once actual INSERT implementation is done, this is only for testing connectivity with DBs
            var localJournalId = "4D1F04EF-F6DC-4B2E-9D56-A46C2585A962";
            Journal dataFromDb = (await connection.QueryAsync<Journal>(GetJournalSql, param: new { id = localJournalId })).FirstOrDefault();

            // TODO: Implement INSERT logic
        }

        public Task<Journal> GetAsync(string deploymentId, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
