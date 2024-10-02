namespace WellSky.Hss.Fhir.Features.Search
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Health.Fhir.Core.Features.Persistence;
    using Microsoft.Health.Fhir.Core.Features.Search;

    public class HssSearchService : SearchService
    {
        public HssSearchService(ILogger<HssSearchService> logger, ISearchOptionsFactory searchOptionsFactory, IFhirDataStore fhirDataStore)
            : base(searchOptionsFactory, fhirDataStore, logger)
        {
        }

        public override Task<IReadOnlyList<string>> GetUsedResourceTypes(CancellationToken cancellationToken)
        {
            return Task.FromResult<IReadOnlyList<string>>(new List<string>(["Patient"]));
        }

        public override Task<SearchResult> SearchAsync(SearchOptions searchOptions, CancellationToken cancellationToken)
        {
            return Task.FromResult(new SearchResult(0, new List<Tuple<string, string>>()));
        }

        protected override Task<SearchResult> SearchForReindexInternalAsync(SearchOptions searchOptions, string searchParameterHash, CancellationToken cancellationToken)
        {
            return Task.FromResult(new SearchResult(0, new List<Tuple<string, string>>()));
        }
    }
}
