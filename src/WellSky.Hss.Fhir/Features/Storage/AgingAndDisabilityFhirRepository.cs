namespace WellSky.Hss.Fhir.Features.Storage
{
    using FhirRepositories;
    using Microsoft.Health.Fhir.Core.Features.Persistence;
    using Microsoft.Health.Fhir.Core.Models;

    public class AgingAndDisabilityFhirRepository(
        IDocumentReferenceRepository documentReferenceRepository,
        ITaskRepository taskRepository)
        : IAgingAndDisabilityFhirRepository
    {
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
