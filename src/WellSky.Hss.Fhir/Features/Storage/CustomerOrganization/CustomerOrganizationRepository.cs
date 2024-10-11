namespace WellSky.Hss.Fhir.Features.Storage.CustomerOrganization
{
    using System.Data;
    using Dapper;
    using EnsureThat;

    public partial class CustomerOrganizationRepository(IDatabaseConnection database) : ICustomerOrganizationRepository
    {
        private readonly IDatabaseConnection _database = EnsureArg.IsNotNull(database, nameof(database));

        public async Task<ClientDatabase> GetClientDatabaseAsync(string deploymentId)
        {
            using IDbConnection connection = _database.GetConnection();

            ClientDatabase database = await connection.QuerySingleOrDefaultAsync<ClientDatabase>(GetClientDatabaseSql,
                new
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
