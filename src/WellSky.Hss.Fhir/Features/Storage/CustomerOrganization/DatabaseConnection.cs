using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace WellSky.Hss.Fhir.Features.Storage.CustomerOrganization
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly ILogger<DatabaseConnection> _logger;
        private readonly HssConfig _hssConfig;

        public DatabaseConnection(ILogger<DatabaseConnection> logger
            , HssConfig hssConfig)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hssConfig = hssConfig;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_hssConfig.CustomerOrganizationDbConnectionString);
        }
    }
}
