﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Health.Fhir.Api.Features.Audit;
using Microsoft.Health.Fhir.Shared.Tests.E2E;

namespace Microsoft.Health.Fhir.Tests.E2E.Rest.Audit
{
    public class StartupWithTraceAuditLogger : StartupBaseForCustomProviders
    {
        public StartupWithTraceAuditLogger(IConfiguration configuration)
            : base(configuration)
        {
        }

        [System.Obsolete]
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.Replace(new ServiceDescriptor(typeof(IAuditLogger), typeof(TraceAuditLogger), ServiceLifetime.Singleton));
        }
    }
}
