// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Health.Fhir.Api.Features.Routing;
using Microsoft.Health.Fhir.Core.Features.Security.Authorization;
using Microsoft.Health.Fhir.Core.Models;

namespace Microsoft.Health.Fhir.Api.Controllers
{
    /// <summary>
    /// Controller responsible for authorization requests
    /// </summary>
    [AllowAnonymous]
    public class WellSkyAuthorizationController : Controller
    {
        private readonly WellSkyOktaAuthorizationService _authorizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WellSkyAuthorizationController" /> class.
        /// </summary>
        public WellSkyAuthorizationController(WellSkyOktaAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Generates an Okta token
        /// </summary>
        [HttpPost]
        [Route(KnownRoutes.GenerateToken)]
        [AllowAnonymous]
        public async Task<ActionResult> GenerateOktaToken([FromBody] WellSkyAuthRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.ClientId) || string.IsNullOrEmpty(request.ClientSecret) || string.IsNullOrEmpty(request.Scope))
            {
                return BadRequest("Invalid request: missing clientId, clientSecret, or scope.");
            }

            var tokenResponse = await _authorizationService.GenerateOktaToken(request);

            if (tokenResponse == null)
            {
                return StatusCode(500, "Error generating token from Okta.");
            }

            return new ContentResult()
            {
                Content = tokenResponse.ToString(Newtonsoft.Json.Formatting.None),
                StatusCode = 200,
                ContentType = "application/json",
            };
        }
    }
}
