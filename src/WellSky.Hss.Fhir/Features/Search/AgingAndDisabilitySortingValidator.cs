namespace WellSky.Hss.Fhir.Features.Search
{
    using Microsoft.Health.Fhir.Core.Features.Search;
    using Microsoft.Health.Fhir.Core.Models;

    public class AgingAndDisabilitySortingValidator : ISortingValidator
    {
        public bool ValidateSorting(IReadOnlyList<(SearchParameterInfo searchParameter, SortOrder sortOrder)> sorting, out IReadOnlyList<string> errorMessages)
        {
            errorMessages = new List<string>();
            return true;
        }
    }
}
