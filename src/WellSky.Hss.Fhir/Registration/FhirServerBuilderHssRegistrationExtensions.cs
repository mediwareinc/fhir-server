using WellSky.Hss.Fhir.Features.Storage.AgingAndDisability;
using WellSky.Hss.Fhir.Features.Storage.AgingAndDisability.InternalRepositories;

namespace WellSky.Hss.Fhir.Registration
{
    using AgingAndDisability;
    using CustomerOrganization;
    using EnsureThat;
    using Features.Search;
    using Features.Storage;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Health.Extensions.DependencyInjection;
    using Microsoft.Health.Fhir.Core.Registration;
    using WellSky.Hss.Fhir.Features.Storage.CustomerOrganization;

    public static class FhirServerBuilderHssRegistrationExtensions
    {
        public static IFhirServerBuilder AddHss(this IFhirServerBuilder fhirServerBuilder, Action<HssConfig> configureAction = null)
        {
            EnsureArg.IsNotNull(fhirServerBuilder, nameof(fhirServerBuilder));
            IServiceCollection services = fhirServerBuilder.Services;

            services.Add(provider =>
                {
                    var config = new HssConfig();
                    provider.GetService<IConfiguration>().GetSection("Hss").Bind(config);
                    configureAction?.Invoke(config);
                    return config;
                })
                .Singleton()
                .AsSelf();

            services.Add<HssFhirDataStore>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<HssSearchService>()
                .Scoped()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<HssSortingValidator>()
                .Singleton()
                .AsSelf()
                .AsImplementedInterfaces();

            services.Add<HssSearchParameterValidator>()
                .Singleton()
                .AsSelf()
                .AsImplementedInterfaces();

            // TODO Aldo: Configuring new HSS storage dependencies here
            // TODO Aldo: Currently all classes under folders in WellSky.Hss.Fhir. Ideally, we want to have separate projs for each of them but need to take care of circular refs.
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

            services.Add<AgingAndDisabilityPatientRepository>()
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

            services.AddSingleton<IHssFhirRepositoryFactory>(incomingServiceProvider =>
            {
                // The incoming serviceProvider isn't the scoped one from the HTTP request, so let's get the one we need so we can handle scoped instances
                // TODO Aldo: concern about needing to import Microsoft.AspNetCore.Http in this proj
                var serviceProvider = new HttpContextAccessor().HttpContext?.RequestServices ?? incomingServiceProvider;
                var scope = serviceProvider.CreateScope();

                var factories = new Dictionary<string, Func<IHssFhirRepository>>
                {
                    ["AD"] = scope.ServiceProvider.GetRequiredService<IAgingAndDisabilityFhirRepository>,
                };

                return new HssFhirRepositoryFactory(factories);
            });

            return fhirServerBuilder;
        }
    }
}
