namespace WellSky.Hss.Fhir.Features.Storage.DataModels
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Journal : BaseModel, IAgingAndDisabilityModel
    {
        public Guid Id { get; set; }

        // TODO Kayla: add required properties
    }
}
