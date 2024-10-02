using System.Data;

namespace WellSky.Hss.Fhir.AgingAndDisability
{
    public interface IDatabaseConnectionFactory
    {
        Task<IDbConnection> GetConnectionByDeploymentIdAsync(string deploymentId);
    }
}
