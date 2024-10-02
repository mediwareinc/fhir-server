using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellSky.Hss.Fhir.Features.Storage.CustomerOrganization
{
    public interface ICustomerOrganizationRepository
    {
        Task<ClientDatabase> GetClientDatabaseAsync(string deploymentId);
        Task<bool> ExistsMappingBetweenTenantAndOktaClientAsync(string deploymentId, string oktaClientId);
    }
}
