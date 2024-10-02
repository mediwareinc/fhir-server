namespace WellSky.Hss.Fhir.Features.Storage.AgingAndDisability.DataModels
{
    public class BaseModel
    {
        public DateTime CreateDatetime { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime LupdateDatetime { get; set; }
        public Guid LupdateUser { get; set; }
    }
}
