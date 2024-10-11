using Microsoft.Health.Fhir.Core.Features.Persistence;

namespace WellSky.Hss.Fhir.Features.Storage
{
    public interface IFhirRepository
    {
        Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation operation, string deploymentId, CancellationToken cancellationToken);
    }
}
