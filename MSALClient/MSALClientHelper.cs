using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.IdentityModel.Abstractions;
using System.Diagnostics;

namespace Moneybase.MSALClient;

public class MSALClientHelper
{
    public AuthenticationResult AuthResult { get; private set; }

    public IPublicClientApplication PublicClientApplication { get; private set; }
    public bool UseEmbedded { get; set; } = false;

    private PublicClientApplicationBuilder PublicClientApplicationBuilder;

    private static string PCANotInitializedExceptionMessage = "The PublicClientApplication needs to be initialized before calling this method. Use InitializePublicClientAppAsync() to initialize.";

    public MSALClientHelper()
    {
        InitializePublicClientApplicationBuilder();
    }
    private void InitializePublicClientApplicationBuilder()
    {
        PublicClientApplicationBuilder = PublicClientApplicationBuilder.Create(Constants.ClientId)
            .WithExperimentalFeatures() // this is for upcoming logger
            .WithB2CAuthority(Constants.AuthoritySignInSignUp)
            .WithLogging(new IdentityLogger(EventLogLevel.Warning), enablePiiLogging: false)    // This is the currently recommended way to log MSAL message. For more info refer to https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/logging. Set Identity Logging level to Warning which is a middle ground
            .WithIosKeychainSecurityGroup(Constants.iOSKeyChainGroup);


        PublicClientApplication = PublicClientApplicationBuilder
            .WithRedirectUri(Constants.RedirectUri)
            .Build();
    }
    public async Task<IAccount> InitializePublicClientAppAsync()
    {
        // Initialize the MSAL library by building a public client application
        PublicClientApplication = PublicClientApplicationBuilder
            .WithRedirectUri(Constants.RedirectUri)
            .Build();
        return await FetchSignedInUserFromCache().ConfigureAwait(false);
    }

    public async Task<AuthenticationResult> GetAuthenticationResultAsync(string[] scopes)
    {
        var existingUser = await FetchSignedInUserFromCache().ConfigureAwait(false);

        return await PublicClientApplication
            .AcquireTokenSilent(scopes, existingUser)
            .ExecuteAsync()
            .ConfigureAwait(false);
    }

    public async Task<AuthenticationResult> FetchUserLoggedIn(string[] scopes)
    {
        var existingUser = await FetchSignedInUserFromCache().ConfigureAwait(false);

        if (existingUser != null)
        {
            AuthResult = await PublicClientApplication
                .AcquireTokenSilent(scopes, existingUser)
                .ExecuteAsync()
                .ConfigureAwait(false);
        }
        return AuthResult;
    }


        public async Task<AuthenticationResult> SignInUserAndAcquireAccessToken(string[] scopes)
    {
        Exception<NullReferenceException>.ThrowOn(() => PublicClientApplication == null, PCANotInitializedExceptionMessage);

        var existingUser = await FetchSignedInUserFromCache().ConfigureAwait(false);

        try
        {
            // 1. Try to sign-in the previously signed-in account
            if (existingUser != null)
            {
                AuthResult = await PublicClientApplication
                    .AcquireTokenSilent(scopes, existingUser)
                    .ExecuteAsync()
                    .ConfigureAwait(false);
            }
            else
            {
                AuthResult = await SignInUserInteractivelyAsync(scopes);
            }
        }
        catch (MsalUiRequiredException ex)
        {
            // A MsalUiRequiredException happened on AcquireTokenSilentAsync. This indicates you need to call AcquireTokenInteractive to acquire a token interactively
            Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

            AuthResult = await PublicClientApplication
                .AcquireTokenInteractive(scopes)
                .ExecuteAsync()
                .ConfigureAwait(false);
        }
        catch (MsalException msalEx)
        {
            Debug.WriteLine($"Error Acquiring Token interactively:{Environment.NewLine}{msalEx}");
        }

        return AuthResult;
    }

    public async Task<AuthenticationResult> SignInUserInteractivelyAsync(string[] scopes, IAccount existingAccount = null)
    {

        Exception<NullReferenceException>.ThrowOn(() => this.PublicClientApplication == null, PCANotInitializedExceptionMessage);

        if (PublicClientApplication == null)
            throw new NullReferenceException();

        if (PublicClientApplication.IsUserInteractive())
        {
            return await PublicClientApplication.AcquireTokenInteractive(scopes)
                .WithParentActivityOrWindow(PlatformConfig.Instance.ParentWindow)
                .ExecuteAsync()
                .ConfigureAwait(false);
        }

        // If the operating system does not have UI (e.g. SSH into Linux), you can fallback to device code, however this
        // flow will not satisfy the "device is managed" CA policy.
        return await PublicClientApplication.AcquireTokenWithDeviceCode(scopes, (dcr) =>
        {
            Console.WriteLine(dcr.Message);
            return Task.CompletedTask;
        }).ExecuteAsync().ConfigureAwait(false);
    }

    public async Task SignOutUserAsync()
    {
        var existingUser = await FetchSignedInUserFromCache().ConfigureAwait(false);
        await SignOutUserAsync(existingUser).ConfigureAwait(false);
    }

    public async Task SignOutUserAsync(IAccount user)
    {
        if (PublicClientApplication == null) return;

        await PublicClientApplication.RemoveAsync(user).ConfigureAwait(false);
    }
    public async Task<IAccount> FetchSignedInUserFromCache()
    {
        //await InitializePublicClientAppAsync();
        //Exception<NullReferenceException>.ThrowOn(() => PublicClientApplication == null, PCANotInitializedExceptionMessage);

        // get accounts from cache
        IEnumerable<IAccount> accounts = await PublicClientApplication.GetAccountsAsync();

        // Error corner case: we should always have 0 or 1 accounts, not expecting > 1
        // This is just an example of how to resolve this ambiguity, which can arise if more apps share a token cache.
        // Note that some apps prefer to use a random account from the cache.
        if (accounts.Count() > 1)
        {
            foreach (var acc in accounts)
            {
                await PublicClientApplication.RemoveAsync(acc);
            }

            return null;
        }

        return accounts.SingleOrDefault();
    }
}
