namespace WellSky.Hss.Fhir.Features.Storage.DataModels
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Journal : BaseModel, IAgingAndDisabilityModel
    {
        public Guid Id { get; set; }

        public Guid? CareplanId { get; set; }

        public Guid? ConsumerId { get; set; }

        public DateTime? EntryDate { get; set; }

        public TimeSpan? EntryTime { get; set; }

        public bool IsRestrictedEntry { get; set; }

        public Guid? JournalTypeId { get; set; }

        public string Notes { get; set; }

        public string Subject { get; set; }
    }
}
