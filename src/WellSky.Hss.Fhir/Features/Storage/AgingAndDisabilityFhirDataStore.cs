namespace WellSky.Hss.Fhir.Features.Storage
{
    using EnsureThat;
    using Microsoft.Extensions.Logging;
    using Microsoft.Health.Core.Features.Security;
    using Microsoft.Health.Fhir.Core.Features.Conformance;
    using Microsoft.Health.Fhir.Core.Features.Persistence;

    internal sealed class AgingAndDisabilityFhirDataStore(
        ILogger<AgingAndDisabilityFhirDataStore> logger,
        IClaimsExtractor claimsExtractor,
        IAgingAndDisabilityFhirRepository agingAndDisabilityFhirRepository)
        : IFhirDataStore, IProvideCapability
    {
        // This claim was manually added for testing, we may want to add an environment suffix later.
        // Needs to be added in appsettings.json -> FhirServer.Security.PrincipalClaims
        private const string DeploymentIdClaim = "hss.fhirserver.deploymentId";
        private readonly ILogger<AgingAndDisabilityFhirDataStore> _logger = EnsureArg.IsNotNull(logger, nameof(logger));

        private readonly IClaimsExtractor _claimsExtractor =
            EnsureArg.IsNotNull(claimsExtractor, nameof(claimsExtractor));

        private readonly IAgingAndDisabilityFhirRepository _fhirRepository =
            EnsureArg.IsNotNull(agingAndDisabilityFhirRepository, nameof(agingAndDisabilityFhirRepository));

        public Task<ResourceWrapper> GetAsync(ResourceKey key, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UpsertOutcome> UpsertAsync(ResourceWrapperOperation resource,
            CancellationToken cancellationToken)
        {
            return await _fhirRepository.UpsertAsync(resource, DeploymentId, cancellationToken);
        }

        public void Build(ICapabilityStatementBuilder builder)
        {
            EnsureArg.IsNotNull(builder, nameof(builder));

            // TODO Aldo: Check if this line could be related to https://wellsky.atlassian.net/browse/MSAMS-16526
            var supportedResources = new List<string> { "CapabilityStatement", "DocumentReference", "Task" };

            builder.PopulateDefaultResourceInteractions(supportedResources)
                .SyncSearchParametersAsync()
                .AddGlobalSearchParameters()
                .SyncProfiles();
        }

        private string DeploymentId => GetClaim(DeploymentIdClaim);

        private string GetClaim(string claimKey)
        {
            return _claimsExtractor.Extract()?.SingleOrDefault(c => c.Key.Equals(claimKey, StringComparison.Ordinal))
                .Value;
        }

        #region Not needed

        public Task<IDictionary<DataStoreOperationIdentifier, DataStoreOperationOutcome>> MergeAsync(
            IReadOnlyList<ResourceWrapperOperation> resources, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<DataStoreOperationIdentifier, DataStoreOperationOutcome>> MergeAsync(
            IReadOnlyList<ResourceWrapperOperation> resources, MergeOptions mergeOptions,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ResourceWrapper>> GetAsync(IReadOnlyList<ResourceKey> keys,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task HardDeleteAsync(ResourceKey key, bool keepCurrentVersion, bool allowPartialSuccess,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task BulkUpdateSearchParameterIndicesAsync(IReadOnlyCollection<ResourceWrapper> resources,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ResourceWrapper> UpdateSearchParameterIndicesAsync(ResourceWrapper resourceWrapper,
            CancellationToken cancellationToken)
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
