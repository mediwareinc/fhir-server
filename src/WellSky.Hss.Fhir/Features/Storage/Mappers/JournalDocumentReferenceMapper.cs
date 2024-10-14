using Hl7.Fhir.Model;
using WellSky.Hss.Fhir.Features.Storage.DataModels;

namespace WellSky.Hss.Fhir.Features.Storage.Mappers
{
    public class JournalDocumentReferenceMapper
    {
        public static Journal ToAdJournal(DocumentReference fhirDocumentReference)
        {
            var adJournal = new Journal();

            adJournal.Id = Guid.NewGuid();
            adJournal.CreateDatetime = DateTime.UtcNow;
            adJournal.CreateUser = new Guid("11111111-1111-1111-1111-111111111111"); // TODO: change this to be a specific MA user
            adJournal.Notes = fhirDocumentReference.Description;
            adJournal.Subject = fhirDocumentReference.Content.FirstOrDefault()?.Attachment.Title; // TODO: note, this is just selecting the first attachment in array. how do we want to really handle this?
            adJournal.IsRestrictedEntry = false; // TODO: check with A&D team purpose of this field
            adJournal.EntryDate = DateTime.UtcNow.Date;
            adJournal.EntryTime = DateTime.UtcNow.TimeOfDay;
            adJournal.CareplanId = null;
            adJournal.JournalTypeId = null; // TODO: Figure out how we want to determine this
            adJournal.LupdateDatetime = DateTime.UtcNow;
            adJournal.LupdateUser = new Guid("11111111-1111-1111-1111-111111111111"); // TODO: change this to be a specific MA user

            var consumerReference = fhirDocumentReference.Subject.Reference;
            var consumerIdString = consumerReference.Substring(consumerReference.LastIndexOf('/') + 1);
            if (Guid.TryParse(consumerIdString, out Guid consumerId))
            {
                adJournal.ConsumerId = consumerId;
            }
            else
            {
                adJournal.ConsumerId = new Guid("24C7DDF4-CB06-49F2-9317-01622D470D27");
                // TODO: how do we want to handle an invalid GUID? also what do we want to do if the GUID isn't a real consumer (do we want to validate that)?
            }

            return adJournal;
        }

        public static DocumentReference ToFhirDocumentReference(Journal adJournal)
        {
            return new DocumentReference();
        }
    }
}
