namespace WellSky.Hss.Fhir.Features.Storage
{
    internal interface IHssFhirRepositoryFactory
    {
        IHssFhirRepository Get(string systemId);
    }
}
