using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using Moneybase.MSALClient;
using Moneybase.Pages;
using Moneybase.Services;
using MoneybaseLibrary.Models;

namespace Moneybase.ViewModels;

public partial class AppLandingViewModel : ViewModelBase
{
    PublicClientSingleton publicClientSingleton;
    private IApiRepository repository;
    public AppLandingViewModel(IApiRepository repo)
    {
        publicClientSingleton = new PublicClientSingleton();
        repository = repo;
    }

    [RelayCommand]
    private async Task SignIn()
    {
        AuthenticationResult authenticationResult = await publicClientSingleton.AcquireTokenSilentAsync();

        if (!authenticationResult.Account.Equals(null))
        {
            FirstTimeUser userIsNew = await repository.UserIsNew(authenticationResult.UniqueId);
            if (userIsNew.IsNew.Equals(false))
                Application.Current.MainPage = new AppShell();
            else
                await Shell.Current.GoToAsync(nameof(SignUpPage));
        }
        else
            await Shell.Current.DisplayAlert("Authentication failed", "Please try again", "Cancel");
    }

    public void CheckIfLoggedIn()
    {
        try
        {
            AuthenticationResult result = publicClientSingleton.CheckIfUserAlreadyLoggedIn(Constants.Scopes).Result;
            if (result != null)
                Application.Current.MainPage = new AppShell();
        }
        catch (Exception)
        {
            Shell.Current.DisplayAlert("Error", "There's been an error trying to automatically log you in. Please try again", "Ok");
        }
    }
}
