namespace WellSky.Hss.Fhir.Features.Storage.DataModels
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ActionItem : BaseModel, IAgingAndDisabilityModel
    {
        public Guid Id { get; set; }

        public Guid? AgencyId { get; set; }

        public Guid ActionId { get; set; }

        public Guid? ConsumerId { get; set; }

        public string Comments { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TimeSpan? EndTime { get; set; }

        public DateTime? FollowupDate { get; set; }

        public int FollowupStatus { get; set; }

        public TimeSpan? FollowupTime { get; set; }

        public Guid? LevelcareLocuscareId { get; set; }

        public Guid? OrganizationId { get; set; }

        public int? OrganizationTypeId { get; set; }

        public string Owner { get; set; }

        public Guid? ProviderId { get; set; }

        public Guid? ReasonCodeId { get; set; }

        public Guid? ReminderId { get; set; }

        public Guid? SiteId { get; set; }

        public DateTime? StartDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public DateTime? StatusDate { get; set; }

        public Guid? StatusCodeId { get; set; }

        public string Subject { get; set; }

        public Guid? SubproviderId { get; set; }
    }
}
