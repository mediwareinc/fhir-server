namespace WellSky.Hss.Fhir.Features.Storage
{
    using EnsureThat;
    using FhirRepositories;
    using Microsoft.Health.Fhir.Core.Features.Persistence;
    using Microsoft.Health.Fhir.Core.Models;

    public class AgingAndDisabilityFhirRepository(
        IDocumentReferenceRepository documentReferenceRepository,
        ITaskRepository taskRepository)
        : IAgingAndDisabilityFhirRepository
    {
        private readonly IDocumentReferenceRepository _documentReferenceRepository =
            EnsureArg.IsNotNull(documentReferenceRepository, nameof(documentReferenceRepository));

        private readonly ITaskRepository _taskRepository = EnsureArg.IsNotNull(taskRepository, nameof(taskRepository));

        public async Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation operation, string deploymentId,
            CancellationToken cancellationToken)
        {
            return operation.Wrapper.ResourceTypeName switch
            {
                KnownResourceTypes.DocumentReference => await _documentReferenceRepository.UpsertAsync(operation,
                    deploymentId, cancellationToken),
                KnownResourceTypes.Task =>
                    await _taskRepository.UpsertAsync(operation, deploymentId, cancellationToken),

                _ => throw new ArgumentException(operation.Wrapper.ResourceTypeName)
            };
        }
    }
}
