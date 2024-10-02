// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using Microsoft.Health.Fhir.Core.Features;

namespace Microsoft.Health.Fhir.Core.Registration
{
    public class HssRuntimeConfiguration : IFhirRuntimeConfiguration
    {
        public string DataStore => KnownDataStores.Hss;

        public bool IsSelectiveSearchParameterSupported => true;

        public bool IsExportBackgroundWorkerSupported => false;

        public bool IsCustomerKeyValidationBackgroundWorkerSupported => false; // TODO Aldo: check usage

        public bool IsTransactionSupported => true;

        public bool IsLatencyOverEfficiencySupported => false;
    }
}
