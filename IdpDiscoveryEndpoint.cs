using AuthorizationApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace AuthorizationApi;

public class IdpDiscoveryEndpoint(ILogger<IdpDiscoveryEndpoint> logger)
{
    private readonly Dictionary<string, string> IdentityProviders = new()
    {
        {"contoso.com", "keycloak"},
        {"contoso.net", "keycloak"},
        {"hotmail.com", "microsoft" },
        {"live.com", "microsoft" }
    };

    public string? GetIdpIdByDomain(string domain) =>
        IdentityProviders.GetValueOrDefault(domain.Trim().ToLower());

    [Function(nameof(IdpDiscoveryEndpoint))]
    [OpenApiOperation(operationId: "idp-discovery", Summary = "Look up for Identity providers for given domain")]
    [OpenApiRequestBody("application/json", typeof(IdpDiscoveryRequest))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IdpDiscoveryResponse), Description = "Response with the IDP id")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "idp-discovery")] HttpRequestData requestData,
        [FromBody] IdpDiscoveryRequest requestBody
    )
    {
        if(string.IsNullOrWhiteSpace(requestBody.Domain))
        {
            logger.LogWarning("No domain provided");
            return new BadRequestObjectResult(new { error = "No domain provided" });
        }

        logger.LogInformation("Looking up for IDP for the domain {domain}", requestBody.Domain);
        var idpId = GetIdpIdByDomain(requestBody.Domain);

        if (idpId is null)
        {
            logger.LogInformation("No IDP found for the domain {domain}", requestBody.Domain);
            return new OkObjectResult(new IdpDiscoveryResponse());
        }

        logger.LogInformation("IDP {idpId} found for the domain {domain}", idpId, requestBody.Domain);
        return new OkObjectResult(new IdpDiscoveryResponse(idpId));
    }
}