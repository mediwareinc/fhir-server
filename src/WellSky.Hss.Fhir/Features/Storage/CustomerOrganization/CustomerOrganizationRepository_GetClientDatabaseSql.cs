using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellSky.Hss.Fhir.CustomerOrganization
{
    public partial class CustomerOrganizationRepository
    {
        private const string GetClientDatabaseSql = @"SELECT 
                                                        d.[Id] as DeploymentId
                                                        , d.[UserId] as UserName
                                                        , d.[DatabaseName]
                                                        , d.[Password]
                                                        , d.[IsActive]
                                                        , db.[Name] as ServerName
                                                        FROM [CustomerOrganization].[dbo].[Deployments] d with(NOLOCK)
                                                        JOIN [CustomerOrganization].[dbo].[DatabaseServers] db with(NOLOCK) ON d.DatabaseServerId = db.[Id]
                                                        WHERE d.[Id] = @deploymentId";
    }
}
