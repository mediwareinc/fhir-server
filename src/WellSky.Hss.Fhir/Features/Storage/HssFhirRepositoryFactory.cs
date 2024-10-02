namespace WellSky.Hss.Fhir.Features.Storage
{
    internal class HssFhirRepositoryFactory : IHssFhirRepositoryFactory
    {
        private readonly Dictionary<string, Func<IHssFhirRepository>> _repositories;

        public HssFhirRepositoryFactory(Dictionary<string, Func<IHssFhirRepository>> repositories)
        {
            _repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
        }

        public IHssFhirRepository Get(string systemId)
        {
            if (!_repositories.TryGetValue(systemId, out var factoryFunc) || factoryFunc is null)
            { throw new ArgumentException($"SystemId '{systemId}' does not belong to any registered application."); } // TODO Aldo: Explore throwing custom exceptions

            return factoryFunc();
        }
    }
}
