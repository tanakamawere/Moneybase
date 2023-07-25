using CommunityToolkit.Maui.Alerts;
using Microsoft.Identity.Client;
using Moneybase.MSALClient;
using Moneybase.Services;
using System.Security.Claims;

namespace Moneybase.Pages;

public partial class AppLandingPage : ContentPage
{
    public IPublicClientApplication IdentityClient { get; set; }
    PublicClientSingleton publicClientSingleton;
    private IApiRepository repository;

    public AppLandingPage(IApiRepository repo)
	{
		InitializeComponent();
        publicClientSingleton = new PublicClientSingleton();
        repository = repo;

        CheckIfLoggedIn();
    }
    private async void OnSignInClicked(object sender, EventArgs e)
    {
        AuthenticationResult authenticationResult = await publicClientSingleton.AcquireTokenSilentAsync();

        string email = authenticationResult.ClaimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Email).Select(x => x.Value).SingleOrDefault().ToString();

        if (!string.IsNullOrEmpty(email))
        {
            if (repository.UserIsNew("tanakamawere15@gmail.com").Equals(true))
                await Shell.Current.GoToAsync(nameof(SignUpPage));
            else
                Application.Current.MainPage = new AppShell();
        }
    }
    private void CheckIfLoggedIn()
    {
        IAccount cachedUserAccount = publicClientSingleton.MSALClientHelper.FetchSignedInUserFromCache().Result;
        if (cachedUserAccount != null)
            Application.Current.MainPage = new AppShell();
    }
}