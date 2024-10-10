using Microsoft.Health.Fhir.Core.Features.Persistence;

namespace WellSky.Hss.Fhir.Features.Storage
{
    public interface IFhirRepository
    {
        Task<ResourceWrapper> GetAsync(ResourceKey key, string deploymentId, CancellationToken cancellationToken);
        Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation operation, string deploymentId, CancellationToken cancellationToken);
    }
}
