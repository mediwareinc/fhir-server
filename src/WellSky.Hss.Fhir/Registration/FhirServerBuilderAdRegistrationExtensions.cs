namespace WellSky.Hss.Fhir.Registration
{
    using EnsureThat;
    using Features.Search;
    using Features.Storage;
    using Features.Storage.CustomerOrganization;
    using Features.Storage.FhirRepositories;
    using Features.Storage.InternalRepositories;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Health.Extensions.DependencyInjection;
    using Microsoft.Health.Fhir.Core.Registration;

    public static class FhirServerBuilderAdRegistrationExtensions
    {
        public static IFhirServerBuilder AddAgingAndDisabilityDataStore(this IFhirServerBuilder fhirServerBuilder, Action<AgingAndDisabilityConfig> configureAction = null)
        {
            EnsureArg.IsNotNull(fhirServerBuilder, nameof(fhirServerBuilder));
            IServiceCollection services = fhirServerBuilder.Services;

            services.Add(provider =>
                {
                    var config = new AgingAndDisabilityConfig();
                    provider.GetService<IConfiguration>().GetSection("AgingAndDisability").Bind(config);
                    configureAction?.Invoke(config);
                    return config;
                })
                .Singleton()
                .AsSelf();

            services.Add<AgingAndDisabilityFhirDataStore>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<AgingAndDisabilitySearchService>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<AgingAndDisabilitySortingValidator>()
                .Singleton()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<AgingAndDisabilitySearchParameterValidator>()
                .Singleton()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<DatabaseConnectionFactory>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<CustomerOrganizationRepository>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<AgingAndDisabilityFhirRepository>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<PatientRepository>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<ConsumerRepository>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<DatabaseConnection>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            return fhirServerBuilder;
        }
    }
}
