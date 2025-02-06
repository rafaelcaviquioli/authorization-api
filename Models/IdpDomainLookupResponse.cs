using System.Text.Json.Serialization;

namespace AuthorizationApi.Models;

public class IdpDomainLookupResponse
{
    // TODO: Try tro change the policy to not need this property
    [JsonPropertyName("extension_has_identity_provider")]
    public bool HasIdentityProvider { get; private set; }

    [JsonPropertyName("extension_identity_provider")]
    public string? IdentityProvider { get; private set; }

    public IdpDomainLookupResponse(string identityProvider)
    {
        HasIdentityProvider = true;
        IdentityProvider = identityProvider;
    }

    public IdpDomainLookupResponse()
    {
    }
}
