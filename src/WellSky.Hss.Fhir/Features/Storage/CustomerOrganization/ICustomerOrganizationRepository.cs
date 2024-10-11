namespace WellSky.Hss.Fhir.Features.Storage.CustomerOrganization
{
    public interface ICustomerOrganizationRepository
    {
        Task<ClientDatabase> GetClientDatabaseAsync(string deploymentId);
        Task<bool> ExistsMappingBetweenTenantAndOktaClientAsync(string deploymentId, string oktaClientId);
    }
}
