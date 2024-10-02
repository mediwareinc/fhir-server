namespace WellSky.Hss.Fhir.Features.Storage
{
    using EnsureThat;
    using Microsoft.Extensions.Logging;
    using Microsoft.Health.Core.Features.Security;
    using Microsoft.Health.Fhir.Core.Features.Conformance;
    using Microsoft.Health.Fhir.Core.Features.Persistence;

    internal sealed class AgingAndDisabilityFhirDataStore : IFhirDataStore, IProvideCapability
    {
        private readonly ILogger<AgingAndDisabilityFhirDataStore> _logger;
        private readonly IClaimsExtractor _claimsExtractor;
        private readonly IAgingAndDisabilityFhirRepository _fhirRepository;

        public AgingAndDisabilityFhirDataStore(ILogger<AgingAndDisabilityFhirDataStore> logger, IClaimsExtractor claimsExtractor, IAgingAndDisabilityFhirRepository agingAndDisabilityFhirRepository)
        {
            _logger = EnsureArg.IsNotNull(logger, nameof(logger));
            _claimsExtractor = EnsureArg.IsNotNull(claimsExtractor, nameof(claimsExtractor));
            _fhirRepository = EnsureArg.IsNotNull(agingAndDisabilityFhirRepository, nameof(agingAndDisabilityFhirRepository));
        }

        public async Task<ResourceWrapper> GetAsync(ResourceKey key, CancellationToken cancellationToken)
        {
            return await _fhirRepository.GetAsync(key, "218"/*DeploymentId*/, cancellationToken);
        }

        public async Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation resource, CancellationToken cancellationToken)
        {
            return await _fhirRepository.UpsertAsync(resource, "218"/*DeploymentId*/, cancellationToken);
        }

        public void Build(ICapabilityStatementBuilder builder)
        {
            EnsureArg.IsNotNull(builder, nameof(builder));

            // SLC: this is how we limit the resource types we want to support
            // The compatibility statements also automatically created based on these settings
            // Any resource type or HTTP action not supported causes HTTP 505 error to be returned to caller
            var supportedResources = new List<string> { "CapabilityStatement", "Organization", "Parameters", "Patient" };

            builder.PopulateDefaultResourceInteractions(supportedResources)
                .SyncSearchParametersAsync()
                .AddGlobalSearchParameters()
                .SyncProfiles();

            //if (_coreFeatures.SupportsBatch)
            //{
            //    // Batch supported added in listedCapability
            //    builder.AddGlobalInteraction(SystemRestfulInteraction.Batch);
            //}

            //if (_coreFeatures.SupportsTransaction)
            //{
            //    // Transaction supported added in listedCapability
            //    builder.AddGlobalInteraction(SystemRestfulInteraction.Transaction);
            //}
        }

        private string DeploymentId => GetClaim("hss.api.deploymentId.dev"); // TODO Aldo: don't hardcode this and build it using env shortname.

        private string GetClaim(string claimKey)
        {
            // TODO Aldo: Extract() is returning empty even when the Principal claims are there. Check PrincipalClaimsExtractor to see why.
            return _claimsExtractor.Extract()?.SingleOrDefault(c => c.Key.Equals(claimKey, StringComparison.Ordinal)).Value;
        }

        #region Not needed

        public Task<IDictionary<DataStoreOperationIdentifier, DataStoreOperationOutcome>> MergeAsync(IReadOnlyList<ResourceWrapperOperation> resources, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<DataStoreOperationIdentifier, DataStoreOperationOutcome>> MergeAsync(IReadOnlyList<ResourceWrapperOperation> resources, MergeOptions mergeOptions, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ResourceWrapper>> GetAsync(IReadOnlyList<ResourceKey> keys, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task HardDeleteAsync(ResourceKey key, bool keepCurrentVersion, bool allowPartialSuccess, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task BulkUpdateSearchParameterIndicesAsync(IReadOnlyCollection<ResourceWrapper> resources, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ResourceWrapper> UpdateSearchParameterIndicesAsync(ResourceWrapper resourceWrapper, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int?> GetProvisionedDataStoreCapacityAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
