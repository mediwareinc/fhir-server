using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellSky.Hss.Fhir.Features.Storage.CustomerOrganization
{
    public class ClientDatabase
    {
        public int DeploymentId { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string EncryptedPassword { get; set; }
        public string Password { get; set; }
    }
}
