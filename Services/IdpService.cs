namespace AuthorizationApi.Services;

public class IdpService : IIdpService
{
    private readonly Dictionary<string, string> IdentityProviders = new()
    {
        {"contoso.com", "keycloak"},
        {"contoso.net", "keycloak"},
    };

    public string? GetIdpIdByDomain(string domain) =>
        IdentityProviders.GetValueOrDefault(domain.Trim().ToLower());
}