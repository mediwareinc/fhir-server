namespace WellSky.Hss.Fhir.Features.Storage.FhirRepositories
{
    using DataModels;
    using EnsureThat;
    using Hl7.Fhir.Model;
    using Hl7.Fhir.Serialization;
    using SqlRepositories;

    public class DocumentReferenceRepository(FhirJsonParser fhirJsonParser, IJournalRepository journalRepository)
        : BaseFhirRepository<Journal, DocumentReference>(fhirJsonParser), IDocumentReferenceRepository
    {
        private readonly IJournalRepository _journalRepository =
            EnsureArg.IsNotNull(journalRepository, nameof(journalRepository));

        protected override Journal MapToAdModel(DocumentReference resource)
        {
            // TODO: call FHIR to A&D mapper
            return new Journal();
        }

        protected override async System.Threading.Tasks.Task AddAsync(string deploymentId, Journal adModel)
        {
            await _journalRepository.AddAsync(deploymentId, adModel);
        }
    }
}
