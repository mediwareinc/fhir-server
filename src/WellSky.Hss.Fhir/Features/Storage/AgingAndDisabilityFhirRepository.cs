namespace WellSky.Hss.Fhir.Features.Storage
{
    using Microsoft.Health.Fhir.Core.Features.Persistence;
    using Microsoft.Health.Fhir.Core.Models;

    public class AgingAndDisabilityFhirRepository : IAgingAndDisabilityFhirRepository
    {
        private readonly IAgingAndDisabilityPatientRepository _patientRepository;

        public AgingAndDisabilityFhirRepository(IAgingAndDisabilityPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<ResourceWrapper> GetAsync(ResourceKey key, string deploymentId, CancellationToken cancellationToken)
        {
            switch (key.ResourceType)
            {
                case KnownResourceTypes.Patient:
                    return await _patientRepository.GetAsync(key, deploymentId, cancellationToken);
            }

            throw new NotImplementedException();
        }

        public async Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation resource, string deploymentId, CancellationToken cancellationToken)
        {
            switch (resource.Wrapper.ResourceTypeName)
            {
                case KnownResourceTypes.Patient:
                    return await _patientRepository.UpsertAsync(resource, deploymentId, cancellationToken);
            }

            throw new NotImplementedException();
        }
    }
}
