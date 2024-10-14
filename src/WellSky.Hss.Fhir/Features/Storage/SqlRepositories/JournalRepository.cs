using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using WellSky.Hss.Fhir.Features.Storage.DataModels;

namespace WellSky.Hss.Fhir.Features.Storage.SqlRepositories
{
    public partial class JournalRepository(
        ILogger<JournalRepository> logger,
        IDatabaseConnectionFactory databaseConnectionFactory)
        : BaseSqlRepository(logger, databaseConnectionFactory), IJournalRepository
    {
        public async Task AddAsync(string deploymentId, Journal journal)
        {
            using IDbConnection connection = await GetConnectionAsync(deploymentId);

            await ExecuteSqlAsync(connection, AddJournalSql, journal, isUpdate: false);
        }
    }
}
