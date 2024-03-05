﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using EnsureThat;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.Health.Core.Features.Context;
using Microsoft.Health.Core.Features.Security;
using Microsoft.Health.Extensions.DependencyInjection;
using Microsoft.Health.Fhir.Api.Features.Context;
using Microsoft.Health.Fhir.Api.Features.Filters;
using Microsoft.Health.Fhir.Api.Features.Formatters;
using Microsoft.Health.Fhir.Api.Features.Health;
using Microsoft.Health.Fhir.Api.Features.Resources;
using Microsoft.Health.Fhir.Api.Features.Resources.Bundle;
using Microsoft.Health.Fhir.Core.Extensions;
using Microsoft.Health.Fhir.Core.Features;
using Microsoft.Health.Fhir.Core.Features.Conformance;
using Microsoft.Health.Fhir.Core.Features.Context;
using Microsoft.Health.Fhir.Core.Features.Health;
using Microsoft.Health.Fhir.Core.Features.Operations;
using Microsoft.Health.Fhir.Core.Features.Persistence;
using Microsoft.Health.Fhir.Core.Features.Security;
using Microsoft.Health.Fhir.Core.Messages.CapabilityStatement;
using Microsoft.Health.Fhir.Core.Models;

namespace Microsoft.Health.Fhir.Api.Modules
{
    /// <summary>
    /// Registration of FHIR components
    /// </summary>
    public class FhirModule : IStartupModule
    {
        /// <inheritdoc />
        public void Load(IServiceCollection services)
        {
            EnsureArg.IsNotNull(services, nameof(services));

#pragma warning disable CS0618 // Type or member is obsolete
            var jsonParser = new FhirJsonParser(new ParserSettings() { PermissiveParsing = true, TruncateDateTimeToDate = true });
#pragma warning restore CS0618 // Type or member is obsolete
            var jsonSerializer = new FhirJsonSerializer();

            var xmlParser = new FhirXmlParser();
            var xmlSerializer = new FhirXmlSerializer();

            services.AddSingleton(jsonParser);
            services.AddSingleton(jsonSerializer);
            services.AddSingleton(xmlParser);
            services.AddSingleton(xmlSerializer);

            FhirPathCompiler.DefaultSymbolTable.AddFhirExtensions();

            ResourceElement SetMetadata(Resource resource, bool isMetaSet, string versionId, DateTimeOffset? lastModified)
            {
                if (!isMetaSet)
                {
                    resource.VersionId = versionId;
                }

                if (lastModified.HasValue && lastModified != DateTimeOffset.MinValue)
                {
                    resource.Meta.LastUpdated = lastModified;
                }

                return resource.ToResourceElement();
            }

            services.AddSingleton<IReadOnlyDictionary<FhirResourceFormat, DeserializeFromString>>(_ =>
            {
                return new Dictionary<FhirResourceFormat, DeserializeFromString>
                {
                    {
                        FhirResourceFormat.Json, (rawResource, version, lastModified) =>
                        {
                            var resource = jsonParser.Parse<Resource>(rawResource.Data);
                            if (rawResource.IsMetaSet)
                            {
                                return resource.ToResourceElement();
                            }

                            return SetMetadata(resource, rawResource.IsMetaSet, version, lastModified);
                        }
                    },
                    {
                        FhirResourceFormat.Xml, (rawResource, version, lastModified) =>
                        {
                            var resource = xmlParser.Parse<Resource>(rawResource.Data);
                            if (rawResource.IsMetaSet)
                            {
                                return resource.ToResourceElement();
                            }

                            return SetMetadata(resource, rawResource.IsMetaSet, version, lastModified);
                        }
                    },
                };
            });

            services.Add<ResourceDeserializer>()
                    .Singleton()
                    .AsSelf()
                    .AsService<IResourceDeserializer>();

            services.Add<FormatterConfiguration>()
                .Singleton()
                .AsSelf()
                .AsService<IPostConfigureOptions<MvcOptions>>();

            services.AddSingleton<IFormatParametersValidator, FormatParametersValidator>();

            services.AddSingleton<OperationOutcomeExceptionFilterAttribute>();
            services.AddSingleton<ValidateFormatParametersAttribute>();
            services.AddSingleton<ValidateExportRequestFilterAttribute>();
            services.AddSingleton<ValidateReindexRequestFilterAttribute>();
            services.AddSingleton<ValidateImportRequestFilterAttribute>();
            services.AddSingleton<ValidateAsyncRequestFilterAttribute>();
            services.AddSingleton<ValidateParametersResourceAttribute>();
            services.AddSingleton<QueryLatencyOverEfficiencyFilterAttribute>();

            // Support for resolve()
            FhirPathCompiler.DefaultSymbolTable.AddFhirExtensions();

            services.Add<FhirJsonInputFormatter>()
                .Singleton()
                .AsSelf()
                .AsService<TextInputFormatter>();

            services.Add<FhirJsonOutputFormatter>()
                .Singleton()
                .AsSelf()
                .AsService<TextOutputFormatter>();

            services.Add<FhirRequestContextAccessor>()
                .Singleton()
                .AsSelf()
                .AsService<RequestContextAccessor<IFhirRequestContext>>();

            services.AddSingleton<CorrelationIdProvider>(_ => () => Guid.NewGuid().ToString());

            // Add conformance provider for implementation metadata.
            services.RemoveServiceTypeExact<SystemConformanceProvider, INotificationHandler<RebuildCapabilityStatement>>()
                .Add<SystemConformanceProvider>()
                .Singleton()
                .AsSelf()
                .AsImplementedInterfaces();

            services.TypesInSameAssembly(KnownAssemblies.All)
                .AssignableTo<IProvideCapability>()
                .Transient()
                .AsService<IProvideCapability>();

            services.AddSingleton<IClaimsExtractor, PrincipalClaimsExtractor>();

            ModelExtensions.SetModelInfoProvider();
            services.Add(_ => ModelInfoProvider.Instance).Singleton().AsSelf().AsImplementedInterfaces();

            // Register a factory to resolve a scope that returns all components that provide capabilities
            services.AddFactory<IScoped<IEnumerable<IProvideCapability>>>();

            // Register pipeline behavior to intercept create/update requests and check presence of provenace header.
            services.Add<ProvenanceHeaderBehavior>().Scoped().AsSelf().AsImplementedInterfaces();
            services.Add<ProvenanceHeaderState>().Scoped().AsSelf().AsImplementedInterfaces();

            // Register pipeline behavior to check service permission for CUD actions on StructuredDefinition,ValueSet,CodeSystem, ConceptMap.
            services.Add<ProfileResourcesBehaviour>().Singleton().AsSelf().AsImplementedInterfaces();

            // Register a router for Bundle requests.
            services.AddSingleton<IRouter, BundleRouter>();

            // Registers a health check for improper behavior
            services.RemoveServiceTypeExact<ImproperBehaviorHealthCheck, INotificationHandler<ImproperBehaviorNotification>>()
                .Add<ImproperBehaviorHealthCheck>()
                .Singleton()
                .AsSelf()
                .AsService<IHealthCheck>()
                .AsService<INotificationHandler<ImproperBehaviorNotification>>();

            services.AddHealthChecks().AddCheck<ImproperBehaviorHealthCheck>(name: "BehaviorHealthCheck");

            services.AddLazy();
            services.AddScoped();

            services.AddTransient(typeof(IScopeProvider<>), typeof(ScopeProvider<>));
        }
    }
}
