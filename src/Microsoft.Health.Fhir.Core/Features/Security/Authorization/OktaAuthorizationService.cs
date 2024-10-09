// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Health.Fhir.Core.Configs;
using Microsoft.Health.Fhir.Core.Models;
using Newtonsoft.Json.Linq;

namespace Microsoft.Health.Fhir.Core.Features.Security.Authorization
{
    /// <summary>
    /// A <see cref="IAuthorizationService"/> that determines access based on the current principal's role memberships and generates an Okta token
    /// </summary>
    public class OktaAuthorizationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationConfiguration _authConfig;

        public OktaAuthorizationService(IHttpClientFactory httpClientFactory, IOptions<AuthenticationConfiguration> authConfigOptions)
        {
            _httpClient = httpClientFactory.CreateClient();
            _authConfig = authConfigOptions.Value;
        }

        public async Task<JObject> GenerateOktaToken(OktaAuthRequest request)
        {
            var tokenEndpoint = new Uri($"{_authConfig.Authority}/v1/token");

            using var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", request.ClientId),
                new KeyValuePair<string, string>("client_secret", request.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("scope", request.Scope),
            });

            HttpResponseMessage response = await _httpClient.PostAsync(tokenEndpoint, formContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var tokenResponse = await response.Content.ReadAsStringAsync();
            return JObject.Parse(tokenResponse);
        }
    }
}
