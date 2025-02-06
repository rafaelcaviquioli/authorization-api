using AuthorizationApi.Models;
using AuthorizationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace AuthorizationApi;

// TODO: Summary
public class IdpDomainLookupEndpoint(ILogger<IdpDomainLookupEndpoint> logger, IIdpService idpService)
{
    [Function(nameof(IdpDomainLookupEndpoint))]
    [OpenApiOperation(operationId: "idp-domain-lookup")]
    [OpenApiRequestBody("application/json", typeof(IdpDomainLookupRequest), Description = "Look up for Identity providers for given domain")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IdpDomainLookupResponse), Description = "Response with the IDP id")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "idp-domain-lookup")] HttpRequestData requestData,
        [FromBody] IdpDomainLookupRequest requestBody
    )
    {
        if(string.IsNullOrWhiteSpace(requestBody.Domain))
        {
            logger.LogWarning("No domain provided");
            return new BadRequestObjectResult(new { error = "No domain provided" });
        }

        logger.LogInformation("Looking up for IDP for the domain {domain}", requestBody.Domain);
        var idpId = idpService.GetIdpIdByDomain(requestBody.Domain);

        if (idpId is null)
        {
            logger.LogInformation("No IDP found for the domain {domain}", requestBody.Domain);
            return new OkObjectResult(new IdpDomainLookupResponse());
        }

        logger.LogInformation("IDP {idpId} found for the domain {domain}", idpId, requestBody.Domain);
        return new OkObjectResult(new IdpDomainLookupResponse(idpId));
    }
}