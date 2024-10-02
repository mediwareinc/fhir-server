using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellSky.Hss.Fhir.CustomerOrganization
{
    public partial class CustomerOrganizationRepository
    {
        private const string ExistsTenantClientMappingSql = @"IF EXISTS(SELECT O.Id
			                                                        FROM [dbo].[Organizations] O (NOLOCK) 
			                                                        INNER JOIN [dbo].[Licenses] L (NOLOCK) ON O.Id = L.OrganizationId
			                                                        INNER JOIN [dbo].[Deployments] D (NOLOCK) ON L.Id = D.LicenseId
			                                                        INNER JOIN [dbo].[OrganizationApiConsumers] OAC (NOLOCK) ON OAC.OrganizationId = O.Id
			                                                        INNER JOIN [dbo].[ApiConsumers] AC (NOLOCK) ON OAC.ApiConsumerId = AC.Id
			                                                        WHERE D.Id = @deploymentId 
			                                                        AND OAC.OktaClientId = @oktaClientId
			                                                        AND O.IsActive = 1
			                                                        AND OAC.IsActive = 1
			                                                        AND AC.IsActive = 1
			                                                        AND L.IsActive = 1
			                                                        AND D.IsActive = 1)
	                                                        SELECT CAST (1 AS bit)
                                                        ELSE
                                                            SELECT CAST (0 AS bit)";
    }
}
