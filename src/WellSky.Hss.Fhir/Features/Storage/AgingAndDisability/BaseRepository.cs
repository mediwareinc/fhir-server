﻿using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace WellSky.Hss.Fhir.AgingAndDisability
{
    public abstract class BaseRepository
    {
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;

        protected ILogger Logger;

        protected BaseRepository(ILogger logger
            , IDatabaseConnectionFactory databaseConnectionFactory)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _databaseConnectionFactory = databaseConnectionFactory ?? throw new ArgumentNullException(nameof(databaseConnectionFactory));
        }

        protected Task<IDbConnection> GetConnectionAsync(string deploymentId)
        {
            return _databaseConnectionFactory.GetConnectionByDeploymentIdAsync(deploymentId);
        }

        protected async Task ExecuteSqlAsync(IDbConnection connection, string sql, object item, bool isUpdate)
        {
            try
            {
                await connection.ExecuteAsync(sql, item);
            }
            catch (SqlException ex)
            {
                HandleException(ex, item, isUpdate);
            }
        }

        private static void HandleException(SqlException ex, object item, bool isUpdate)
        {
            // SQL Server error numbers: https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/database-engine-events-and-errors?view=sql-server-ver16
            switch (ex.Number)
            {
                case 2627:
                    if (isUpdate)
                    { throw new ArgumentException($"Item already exists: {item}", ex); }  // TODO Aldo: Explore throwing custom exceptions

                    throw new ArgumentException($"Item already exists: {item}", ex);  // TODO Aldo: Explore throwing custom exceptions
                default:
                    throw new InvalidOperationException("Repository Error", ex);
            }
        }
    }
}
