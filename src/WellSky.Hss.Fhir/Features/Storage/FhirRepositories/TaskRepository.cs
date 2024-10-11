namespace WellSky.Hss.Fhir.Features.Storage.FhirRepositories
{
    using DataModels;
    using Hl7.Fhir.Model;
    using Hl7.Fhir.Serialization;
    using Microsoft.Health.Fhir.Core.Features.Persistence;
    using SqlRepositories;

    public class TaskRepository(
        FhirJsonParser fhirJsonParser,
        IActionItemRepository actionItemRepository)
        : ITaskRepository
    {
        public async Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation operation, string deploymentId, CancellationToken cancellationToken)
        {
            Task resource = await fhirJsonParser.ParseAsync<Task>(operation.Wrapper.RawResource.Data);

            // TODO: call FHIR to A&D mapper
            var actionItem = new ActionItem();

            // Currently UpsertAsync will only be used for Create requests
            if (operation.Wrapper.Request.Method == HttpMethod.Post.ToString())
            {
                await actionItemRepository.AddAsync(deploymentId, actionItem);
                return new UpsertOutcome(operation.Wrapper, SaveOutcomeType.Created);
            }

            return new UpsertOutcome(operation.Wrapper, SaveOutcomeType.Created);
        }
    }
}
