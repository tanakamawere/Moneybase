using Microsoft.Identity.Client;

namespace Moneybase.MSALClient;
public class PublicClientSingleton
{
    public MSALClientHelper MSALClientHelper;

    public PublicClientSingleton()
    {
        MSALClientHelper = new MSALClientHelper();
    }
    public bool UseEmbedded { get; set; } = false;
    public async Task<AuthenticationResult> AcquireTokenSilentAsync()
    {
        // Get accounts by policy
        return await AcquireTokenSilentAsync(GetScopes()).ConfigureAwait(false);
    }

    public async Task<AuthenticationResult> AcquireAuthenticationResult(string[] scopes)
    {
        return await MSALClientHelper.GetAuthenticationResultAsync(scopes).ConfigureAwait(false);
    }

    public async Task<AuthenticationResult> AcquireTokenSilentAsync(string[] scopes)
    {
        return await MSALClientHelper.SignInUserAndAcquireAccessToken(scopes).ConfigureAwait(false);
    }
    internal async Task<AuthenticationResult> AcquireTokenInteractiveAsync(string[] scopes)
    {
        MSALClientHelper.UseEmbedded = UseEmbedded;
        return await MSALClientHelper.SignInUserInteractivelyAsync(scopes).ConfigureAwait(false);
    }

    public async Task<AuthenticationResult> CheckIfUserAlreadyLoggedIn(string[] scopes)
    {
        return await MSALClientHelper.FetchUserLoggedIn(scopes).ConfigureAwait(false);
    }


    internal async Task SignOutAsync()
    {
        await MSALClientHelper.SignOutUserAsync().ConfigureAwait(false);
    }
    internal string[] GetScopes()
    {
        return Constants.Scopes;
    }
}
