using System.Text.Json.Serialization;

namespace AuthorizationApi.Models;

public class IdpDiscoveryResponse
{
    // The prefix extension_ is implied by AD B2C REST integration specification
    [JsonPropertyName("extension_has_identity_provider")]
    public bool HasIdentityProvider { get; private set; }

    [JsonPropertyName("extension_identity_provider")]
    public string? IdentityProvider { get; private set; }

    public IdpDiscoveryResponse(string identityProvider)
    {
        HasIdentityProvider = true;
        IdentityProvider = identityProvider;
    }

    public IdpDiscoveryResponse()
    {
    }
}
