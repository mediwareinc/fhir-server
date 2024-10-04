namespace WellSky.Hss.Fhir.Features.Storage.FhirRepositories
{
    using Hl7.Fhir.Serialization;
    using InternalRepositories;
    using Mappers;
    using Microsoft.Health.Fhir.Core.Features.Persistence;
    using Microsoft.Health.Fhir.Core.Models;

    public class PatientRepository : IPatientRepository
    {
        private readonly FhirJsonSerializer _fhirJsonSerializer;
        private readonly IConsumerRepository _consumerRepository;

        public PatientRepository(FhirJsonSerializer fhirJsonSerializer, IConsumerRepository consumerRepository)
        {
            _fhirJsonSerializer = fhirJsonSerializer;
            _consumerRepository = consumerRepository;
        }

        // Aldo: Implementing this method to test connectivity with CustOrgDB and A%D
        public async Task<ResourceWrapper> GetAsync(ResourceKey key, string deploymentId, CancellationToken cancellationToken)
        {
            var consumer = await _consumerRepository.GetAsync(deploymentId, Guid.Parse(key.Id));
            var patient = ConsumerMapper.Map(consumer);
            var resourceJson = await _fhirJsonSerializer.SerializeToStringAsync(patient);

            return new ResourceWrapper(
                key.Id,
                "1", // Aldo: how are we planning to handle versioning?
                KnownResourceTypes.Patient,
                new RawResource(resourceJson, FhirResourceFormat.Json, true),
                new ResourceRequest("GET"),
                DateTimeOffset.UtcNow,
                false,
                null,
                null,
                null);
        }

        public Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation resource, string deploymentId, CancellationToken cancellationToken)
        {
            // TODO Aldo: Research how to map UpsertOutcome result

            throw new NotImplementedException();
        }
    }
}
