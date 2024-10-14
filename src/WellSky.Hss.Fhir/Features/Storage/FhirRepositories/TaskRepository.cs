using WellSky.Hss.Fhir.Features.Storage.Mappers;

namespace WellSky.Hss.Fhir.Features.Storage.FhirRepositories
{
    using DataModels;
    using EnsureThat;
    using Hl7.Fhir.Model;
    using Hl7.Fhir.Serialization;
    using SqlRepositories;

    public class TaskRepository(FhirJsonParser fhirJsonParser, IActionItemRepository actionItemRepository)
        : BaseFhirRepository<ActionItem, Task>(fhirJsonParser), ITaskRepository
    {
        private readonly IActionItemRepository _actionItemRepository =
            EnsureArg.IsNotNull(actionItemRepository, nameof(actionItemRepository));

        protected override ActionItem MapToAdModel(Task resource)
        {
            ActionItem adActionItem = ActionItemTaskMapper.ToAdActionItem(resource);

            return adActionItem;
        }

        protected override async System.Threading.Tasks.Task AddAsync(string deploymentId, ActionItem adModel)
        {
            await _actionItemRepository.AddAsync(deploymentId, adModel);
        }
    }
}
