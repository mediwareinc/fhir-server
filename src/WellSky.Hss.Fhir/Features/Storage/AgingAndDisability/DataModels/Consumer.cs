// ReSharper disable IdentifierTypo

using System.Diagnostics.CodeAnalysis;

namespace WellSky.Hss.Fhir.Features.Storage.AgingAndDisability.DataModels
{
    [ExcludeFromCodeCoverage]
    public class Consumer : BaseModel, IAgingAndDisabilityModel
    {
        // ReSharper disable once InconsistentNaming
        private int? cogImpairmentCode;
        public Guid Id { get; set; }
        public Guid? AgencyUuid { get; set; }
        public string AgencyDescription { get; set; }
        public bool IsClientGroup { get; set; }
        public DateTime? Dob { get; set; }
        public short? Age { get; set; }
        public string Gender { get; set; }
        public short? GenderIdentity { get; set; }
        public string ResTownName { get; set; }
        public string ResState { get; set; }
        public string ResZip { get; set; }
        public string ResCounty { get; set; }
        public string ResMunicipality { get; set; }
        public DateTime? DateRegistered { get; set; }
        public byte? IsRural { get; set; }
        public byte? RucaIsRural { get; set; }
        public byte? IsLivesAlone { get; set; }
        public byte? IsInPoverty { get; set; }
        public bool IsTribal { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDupMail { get; set; }
        public bool IsFrail { get; set; }
        public bool IsFemaleHohh { get; set; }
        public bool IsAbusedNegExp { get; set; }
        public bool IsUsdaMealEligible { get; set; }
        public bool IsHomebound { get; set; }
        public bool UnderstandsEnglish { get; set; }
        public bool IsUsCitizen { get; set; }
        public bool IsStateResident { get; set; }
        public bool IsVeteran { get; set; }
        public bool IsVeteranDependent { get; set; }
        public bool IsSsi { get; set; }
        public bool IsMedicareEligible { get; set; }
        public string ReferredBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StatusDate { get; set; }
        public string Ethnicity { get; set; }
        public int EmpStatusCode { get; set; }

        // return 0 , if null since legacy is giving exception if the null value is stored in DB for this property
        public int? CogImpairmentCode
        {
            get => cogImpairmentCode ?? 0;
            set => cogImpairmentCode = value;
        }

        public Guid? MaritalStatusUuid { get; set; }
        public string UptCardId { get; set; }
        public string HomePhone { get; set; }
        public string ResAddress1 { get; set; }
        public string ResAddress2 { get; set; }
        public string PrimaryCareManager { get; set; }
        public string PrimaryPhone { get; set; }
        public string MjmId { get; set; }
        public string Language { get; set; }
        public Client Client { get; set; }
    }
}
