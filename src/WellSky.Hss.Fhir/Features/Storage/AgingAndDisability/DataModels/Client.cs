// ReSharper disable IdentifierTypo

namespace WellSky.Hss.Fhir.Features.Storage.AgingAndDisability.DataModels
{
    public class Client : BaseModel, IAgingAndDisabilityModel
    {
        public Guid Id { get; set; }
        public double ClientId { get; set; }
        public string Salutation { get; set; }
        public string Suffix { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mi { get; set; }
        public string FullName { get; set; }
        public string Ssn { get; set; }
        public string MedicaIdNo { get; set; }
        public Consumer Consumer { get; set; }
    }
}
