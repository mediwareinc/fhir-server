using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace WellSky.Hss.Fhir.Features.Storage.CustomerOrganization
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly ILogger<DatabaseConnection> _logger;
        private readonly AgingAndDisabilityConfig _agingAndDisabilityConfig;

        public DatabaseConnection(ILogger<DatabaseConnection> logger
            , AgingAndDisabilityConfig agingAndDisabilityConfig)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _agingAndDisabilityConfig = agingAndDisabilityConfig;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_agingAndDisabilityConfig.CustomerOrganizationDbConnectionString);
        }
    }
}
