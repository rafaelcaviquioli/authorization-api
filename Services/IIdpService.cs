namespace AuthorizationApi.Services;

public interface IIdpService
{
    public string? GetIdpIdByDomain(string domain);
}
