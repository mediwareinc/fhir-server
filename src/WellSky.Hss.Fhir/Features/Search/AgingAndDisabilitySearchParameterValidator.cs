namespace WellSky.Hss.Fhir.Features.Search
{
    using Microsoft.Health.Fhir.Core.Features.Search;
    using Microsoft.Health.Fhir.Core.Models;

    internal class AgingAndDisabilitySearchParameterValidator : IDataStoreSearchParameterValidator
    {
        public bool ValidateSearchParameter(SearchParameterInfo searchParameter, out string errorMessage)
        {
            errorMessage = string.Empty;
            return true;
        }
    }
}
