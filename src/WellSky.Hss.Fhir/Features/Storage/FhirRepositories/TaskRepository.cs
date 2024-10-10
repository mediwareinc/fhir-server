﻿namespace WellSky.Hss.Fhir.Features.Storage.FhirRepositories
{
    using DataModels;
    using Hl7.Fhir.Model;
    using Hl7.Fhir.Serialization;
    using InternalRepositories;
    using Microsoft.Health.Fhir.Core.Features.Persistence;
    using Microsoft.Health.Fhir.Core.Models;

    public class TaskRepository(
        FhirJsonSerializer fhirJsonSerializer,
        IActionItemRepository actionItemRepository)
        : ITaskRepository
    {
        // Not planning to have a full implementation for now, only for testing connectivity with DBs
        public async Task<ResourceWrapper> GetAsync(ResourceKey key, string deploymentId, CancellationToken cancellationToken)
        {
            ActionItem actionItem = await actionItemRepository.GetAsync(deploymentId, Guid.Parse(key.Id));

            // TODO: Call A&D to FHIR mapper
            var resource = new Task
            {
                Id = actionItem.Id.ToString()
            };

            var resourceJson = await fhirJsonSerializer.SerializeToStringAsync(resource);

            return new ResourceWrapper(
                key.Id,
                "1",
                KnownResourceTypes.Task,
                new RawResource(resourceJson, FhirResourceFormat.Json, true),
                new ResourceRequest("GET"),
                DateTimeOffset.UtcNow,
                false,
                null,
                null,
                null);
        }

        public async Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation resource, string deploymentId, CancellationToken cancellationToken)
        {
            // TODO: call FHIR to A&D mapper
            var actionItem = new ActionItem();

            // TODO Aldo: how to know this is called by Create and not Update?
            await actionItemRepository.AddAsync(deploymentId, actionItem);

            return new UpsertOutcome(resource.Wrapper, SaveOutcomeType.Created);
        }
    }
}
