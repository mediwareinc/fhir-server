namespace WellSky.Hss.Fhir.Features.Storage
{
    using System.Data;
    using System.Transactions;
    using CustomerOrganization;
    using EnsureThat;
    using Microsoft.Data.SqlClient;

    public sealed class DatabaseConnectionFactory(ICustomerOrganizationRepository customerRepository)
        : IDatabaseConnectionFactory
    {
        private readonly ICustomerOrganizationRepository _repository =
            EnsureArg.IsNotNull(customerRepository, nameof(customerRepository));

        public async Task<IDbConnection> GetConnectionByDeploymentIdAsync(string deploymentId)
        {
            return new SqlConnection(await GetConnectionStringAsync(deploymentId));
        }

        // TODO Aldo: Explore throwing custom exceptions
        private async Task<string> GetConnectionStringAsync(string deploymentId)
        {
            using var transaction =
                new TransactionScope(TransactionScopeOption.Suppress, TransactionScopeAsyncFlowOption.Enabled);

            var database = await _repository.GetClientDatabaseAsync(deploymentId);

            transaction.Complete();

            if (database == null)
            {
                throw new ArgumentException($"DeploymentId value '{deploymentId}' does not match any database.");
            }

            var connectionString = BuildConnectionStringFromDatabase(database);

            return connectionString;
        }

        private static string BuildConnectionStringFromDatabase(ClientDatabase database)
        {
            // TODO Aldo: Adding TrustServerCertificate=True; only for local testing
            return
                $"Server={database.ServerName};Database={database.DatabaseName};User Id={database.UserName};Password={database.Password};TrustServerCertificate=True;";
        }
    }
}
