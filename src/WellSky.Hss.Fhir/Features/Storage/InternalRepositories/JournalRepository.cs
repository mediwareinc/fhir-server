namespace WellSky.Hss.Fhir.Features.Storage.InternalRepositories
{
    using System.Data;
    using Dapper;
    using DataModels;
    using Microsoft.Extensions.Logging;

    public partial class JournalRepository(
        ILogger<JournalRepository> logger,
        IDatabaseConnectionFactory databaseConnectionFactory)
        : BaseRepository(logger, databaseConnectionFactory), IJournalRepository
    {
        public async Task AddAsync(string deploymentId, Journal journal)
        {
            using IDbConnection connection = await GetConnectionAsync(deploymentId);

            // TODO: Implement Insert logic
        }

        // Not planning to have a full implementation for now, only for testing connectivity with DBs
        public async Task<Journal> GetAsync(string deploymentId, Guid id)
        {
            using IDbConnection connection = await GetConnectionAsync(deploymentId);

            Journal journal = (await connection.QueryAsync<Journal>(GetJournalSql, param: new { id })).FirstOrDefault();

            return journal;
        }
    }
}
