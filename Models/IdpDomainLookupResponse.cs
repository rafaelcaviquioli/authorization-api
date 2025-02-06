using System.Text.Json.Serialization;

namespace AuthorizationApi.Models;

public class IdpDomainLookupResponse
{
    // TODO: Try tro change the policy to not need this property
    [JsonPropertyName("extension_has_identity_provider")]
    public bool HasIdentityProvider { get; private set; }

    // TODO: rename it to provider singular?
    [JsonPropertyName("extension_identity_providers")]
    public string? IdentityProviders { get; private set; }

    public IdpDomainLookupResponse(string identityProviders)
    {
        HasIdentityProvider = true;
        IdentityProviders = identityProviders;
    }

    public IdpDomainLookupResponse()
    {
    }
}
