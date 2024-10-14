using WellSky.Hss.Fhir.Features.Storage.DataModels;
using Task = Hl7.Fhir.Model.Task;

namespace WellSky.Hss.Fhir.Features.Storage.Mappers
{
    public class ActionItemTaskMapper
    {
        public static ActionItem ToAdActionItem(Task fhirTask)
        {
            return new ActionItem();
        }

        public static Task ToFhirTask(ActionItem adActionItem)
        {
            return new Task();
        }
    }
}
