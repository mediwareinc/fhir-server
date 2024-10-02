﻿using Microsoft.Health.Fhir.Core.Features.Persistence;

namespace WellSky.Hss.Fhir.Features.Storage
{
    public interface IHssFhirRepository
    {
        Task<ResourceWrapper> GetAsync(ResourceKey key, string deploymentId, CancellationToken cancellationToken);
        Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation resource, string deploymentId, CancellationToken cancellationToken);
    }
}
