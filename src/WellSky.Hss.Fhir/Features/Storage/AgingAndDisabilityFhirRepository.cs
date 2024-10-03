namespace WellSky.Hss.Fhir.Features.Storage
{
    using Microsoft.Health.Fhir.Core.Features.Persistence;
    using Microsoft.Health.Fhir.Core.Models;

    public class AgingAndDisabilityFhirRepository : IAgingAndDisabilityFhirRepository
    {
        private readonly IPatientRepository _patientRepository;

        public AgingAndDisabilityFhirRepository(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<ResourceWrapper> GetAsync(ResourceKey key, string deploymentId, CancellationToken cancellationToken)
        {
            return key.ResourceType switch
            {
                // Aldo: testing DB conn with Patient Read
                KnownResourceTypes.Patient => await _patientRepository.GetAsync(key, deploymentId, cancellationToken),

                _ => throw new ArgumentException(key.ResourceType)
            };
        }

        public Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation resource, string deploymentId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
