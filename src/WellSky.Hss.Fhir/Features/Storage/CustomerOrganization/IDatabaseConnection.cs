using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellSky.Hss.Fhir.Features.Storage.CustomerOrganization
{
    public interface IDatabaseConnection
    {
        IDbConnection GetConnection();
    }
}
