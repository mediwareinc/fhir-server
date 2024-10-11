namespace WellSky.Hss.Fhir.Features.Storage.CustomerOrganization
{
    using System.Data;
    using EnsureThat;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Logging;

    public class DatabaseConnection(
        ILogger<DatabaseConnection> logger,
        AgingAndDisabilityConfig agingAndDisabilityConfig)
        : IDatabaseConnection
    {
        private readonly ILogger<DatabaseConnection> _logger = EnsureArg.IsNotNull(logger, nameof(logger));

        private readonly AgingAndDisabilityConfig _agingAndDisabilityConfig =
            EnsureArg.IsNotNull(agingAndDisabilityConfig, nameof(agingAndDisabilityConfig));

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_agingAndDisabilityConfig.CustomerOrganizationDbConnectionString);
        }
    }
}
