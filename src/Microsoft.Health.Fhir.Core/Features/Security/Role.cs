// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Azure.Core;
using Azure.Storage.Sas;
using EnsureThat;

namespace Microsoft.Health.Fhir.Core.Features.Security
{
    // Kayla added for POC
    public class Role
    {
        public Role(string name, DataActions allowedDataActions, string scope, string[] resourceTypes)
        {
            EnsureArg.IsNotNullOrWhiteSpace(name, nameof(name));
            EnsureArg.Is(scope, "/", nameof(scope)); // until we support data slices

            Name = name;
            AllowedDataActions = allowedDataActions;
            Scope = scope;
            ResourceTypes = resourceTypes; // TODO: WellSky Added
        }

        public string Name { get; }

        public DataActions AllowedDataActions { get; }

        public string Scope { get; }

        #pragma warning disable CA1819
        public string[] ResourceTypes { get; } // TODO: WellSky Added
        #pragma warning disable CA1819
    }
}
