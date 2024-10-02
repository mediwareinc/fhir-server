using System.Data;

namespace WellSky.Hss.Fhir.Features.Storage
{
    public interface IDatabaseConnectionFactory
    {
        Task<IDbConnection> GetConnectionByDeploymentIdAsync(string deploymentId);
    }
}
