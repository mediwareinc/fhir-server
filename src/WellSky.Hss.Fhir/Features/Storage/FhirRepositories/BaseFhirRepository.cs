namespace WellSky.Hss.Fhir.Features.Storage.FhirRepositories
{
    using DataModels;
    using EnsureThat;
    using Hl7.Fhir.Model;
    using Hl7.Fhir.Serialization;
    using Microsoft.Health.Fhir.Core.Features.Persistence;
    using Task = Task;

    public abstract class BaseFhirRepository<TAdModel, TResource>(FhirJsonParser fhirJsonParser)
        where TAdModel : IAgingAndDisabilityModel
        where TResource : Resource
    {
        private readonly FhirJsonParser _fhirJsonParser = EnsureArg.IsNotNull(fhirJsonParser, nameof(fhirJsonParser));

        public async Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation operation, string deploymentId,
            CancellationToken cancellationToken)
        {
            TResource resource = await _fhirJsonParser.ParseAsync<TResource>(operation.Wrapper.RawResource.Data);
            TAdModel adModel = MapToAdModel(resource);

            // Currently UpsertAsync will only be used for Create requests
            if (operation.Wrapper.Request.Method == HttpMethod.Post.ToString())
            {
                await AddAsync(deploymentId, adModel); // TODO: do we want to return the request model or the model actually inserted into the DB after mapping?
                return new UpsertOutcome(operation.Wrapper, SaveOutcomeType.Created);
            }

            throw new NotSupportedException();
        }

        protected abstract TAdModel MapToAdModel(TResource resource);
        protected abstract Task AddAsync(string deploymentId, TAdModel adModel);
    }
}
