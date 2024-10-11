namespace WellSky.Hss.Fhir.Features.Storage.CustomerOrganization
{
    using System.Data;

    public interface IDatabaseConnection
    {
        IDbConnection GetConnection();
    }
}
