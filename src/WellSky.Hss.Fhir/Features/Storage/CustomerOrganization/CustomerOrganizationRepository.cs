using Dapper;
using WellSky.Hss.Fhir.Features.Storage.CustomerOrganization;

namespace WellSky.Hss.Fhir.CustomerOrganization
{
    public partial class CustomerOrganizationRepository : ICustomerOrganizationRepository
    {
        private readonly IDatabaseConnection _database;

        public CustomerOrganizationRepository(IDatabaseConnection database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task<ClientDatabase> GetClientDatabaseAsync(string deploymentId)
        {
            using var connection = _database.GetConnection();

            var database = await connection.QuerySingleOrDefaultAsync<ClientDatabase>(GetClientDatabaseSql, new
            {
                deploymentId
            });

            return database;
        }

        public async Task<bool> ExistsMappingBetweenTenantAndOktaClientAsync(string deploymentId, string oktaClientId)
        {
            using var connection = _database.GetConnection();
            var exists = await connection.ExecuteScalarAsync<bool>(ExistsTenantClientMappingSql, new
            {
                deploymentId,
                oktaClientId
            });

            return exists;
        }
    }
}
