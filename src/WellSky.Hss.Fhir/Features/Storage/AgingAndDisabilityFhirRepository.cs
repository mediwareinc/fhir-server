namespace WellSky.Hss.Fhir.Features.Storage
{
    using Microsoft.Health.Fhir.Core.Features.Persistence;
    using Microsoft.Health.Fhir.Core.Models;

    public class AgingAndDisabilityFhirRepository(
        IDocumentReferenceRepository documentReferenceRepository,
        ITaskRepository taskRepository)
        : IAgingAndDisabilityFhirRepository
    {
        // Not planning to have a full implementation for now, only for testing connectivity with DBs
        public async Task<ResourceWrapper> GetAsync(ResourceKey key, string deploymentId, CancellationToken cancellationToken)
        {
            return key.ResourceType switch
            {
                KnownResourceTypes.DocumentReference => await documentReferenceRepository.GetAsync(key, deploymentId, cancellationToken),
                KnownResourceTypes.Task => await taskRepository.GetAsync(key, deploymentId, cancellationToken),

                _ => throw new ArgumentException(key.ResourceType)
            };
        }

        public async Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation operation, string deploymentId, CancellationToken cancellationToken)
        {
            return operation.Wrapper.ResourceTypeName switch
            {
                KnownResourceTypes.DocumentReference => await documentReferenceRepository.UpsertAsync(operation, deploymentId, cancellationToken),
                KnownResourceTypes.Task => await taskRepository.UpsertAsync(operation, deploymentId, cancellationToken),

                _ => throw new ArgumentException(operation.Wrapper.ResourceTypeName)
            };
        }
    }
}
