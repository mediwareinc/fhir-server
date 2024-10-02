using Microsoft.Health.Fhir.Core.Features.Persistence;
using Microsoft.Health.Fhir.Core.Models;
using WellSky.Hss.Fhir.Features.Storage.AgingAndDisability.InternalRepositories;

namespace WellSky.Hss.Fhir.Features.Storage.AgingAndDisability
{
    public class AgingAndDisabilityPatientRepository : IAgingAndDisabilityFhirRepository
    {
        private readonly IConsumerRepository _consumerRepository;

        public AgingAndDisabilityPatientRepository(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        public async Task<ResourceWrapper> GetAsync(ResourceKey key, string deploymentId, CancellationToken cancellationToken)
        {
            var consumer = await _consumerRepository.GetAsync(deploymentId, Guid.Parse(key.Id));

            // TODO Aldo: Research how to map ResourceWrapper, do we have everything we need from DB?
            return new ResourceWrapper(
                key.Id, // TODO Aldo: Should use ReferenceId convention from here?
                "1", // TODO Aldo: how are we planning to get version?
                KnownResourceTypes.Patient,
                new RawResource("resource data as JSON here", FhirResourceFormat.Json, true),
                new ResourceRequest("GET"),
                DateTimeOffset.UtcNow,
                false,
                null,
                null,
                null);
        }

        // TODO Aldo: Assuming this method will only be called from Create requests for now
        public async Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation resource, string deploymentId, CancellationToken cancellationToken)
        {
            // TODO Aldo: Research how to map ResourceWrapper, do we have everything we need from DB?
            var existingResource = new ResourceWrapper(
                resource.Wrapper.ResourceId, // TODO Aldo: Should use ReferenceId convention from here?
                "1", // TODO Aldo: how are we planning to get version?
                KnownResourceTypes.Patient,
                new RawResource("resource data as JSON here", FhirResourceFormat.Json, true),
                new ResourceRequest("POST"),
                DateTimeOffset.UtcNow,
                false,
                null,
                null,
                null);
            return new UpsertOutcome(existingResource, SaveOutcomeType.Created); 
        }
    }
}
