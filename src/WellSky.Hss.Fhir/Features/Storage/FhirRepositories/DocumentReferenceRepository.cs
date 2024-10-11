namespace WellSky.Hss.Fhir.Features.Storage.FhirRepositories
{
    using DataModels;
    using Hl7.Fhir.Model;
    using Hl7.Fhir.Serialization;
    using Microsoft.Health.Fhir.Core.Features.Persistence;
    using SqlRepositories;

    public class DocumentReferenceRepository(
        FhirJsonParser fhirJsonParser,
        IJournalRepository journalRepository)
        : IDocumentReferenceRepository
    {
        public async Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation operation, string deploymentId, CancellationToken cancellationToken)
        {
            DocumentReference resource = await fhirJsonParser.ParseAsync<DocumentReference>(operation.Wrapper.RawResource.Data);

            // TODO: call FHIR to A&D mapper
            var journal = new Journal();

            // Currently UpsertAsync will only be used for Create requests
            if (operation.Wrapper.Request.Method == HttpMethod.Post.ToString())
            {
                await journalRepository.AddAsync(deploymentId, journal);
                return new UpsertOutcome(operation.Wrapper, SaveOutcomeType.Created);
            }

            throw new NotSupportedException();
        }
    }
}
