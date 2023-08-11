using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using Moneybase.MSALClient;
using Moneybase.Pages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

public partial class AppLandingViewModel : ViewModelBase
{
    public AppLandingViewModel(IApiRepository repo, IPopupNavigation popup)
    {
        repository = repo;
        mopupNavigation = popup;
        loadingPopup = new LoadingPopup();
    }

    [RelayCommand]
    async Task GoToPage()
    {
        await Shell.Current.GoToAsync(nameof(SecurityPINPage));
    }

    [RelayCommand]
    private async Task SignIn()
    {
        await mopupNavigation.PushAsync(loadingPopup);
        AuthenticationResult authenticationResult = await publicClientSingleton.AcquireTokenSilentAsync();

        if (!authenticationResult.Account.Equals(null))
        {
            FirstTimeUser userIsNew = await repository.UserIsNew(authenticationResult.UniqueId);
            if (userIsNew.IsNew.Equals(false))
                await Shell.Current.GoToAsync(nameof(SecurityPINPage));
            else
            {
                string emailYangu = authenticationResult.ClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == "emails").Value.ToString();
                await Shell.Current.GoToAsync($"{nameof(OnboardingDetailsPage)}?onboardingEmail={emailYangu}");
            }
            await mopupNavigation.PopAsync(true);
        }
        else
        {
            await Shell.Current.DisplayAlert("Authentication failed", "Please try again", "Cancel");
            await mopupNavigation.PopAsync(true);
        }
    }

    public void CheckIfLoggedIn()
    {
        try
        {
            AuthenticationResult result = publicClientSingleton.CheckIfUserAlreadyLoggedIn(Constants.Scopes).Result;
            if (result != null)
                Shell.Current.GoToAsync(nameof(SecurityPINPage));
        }
        catch (Exception)
        {
            Shell.Current.DisplayAlert("Error", "There's been an error trying to automatically log you in. Please try again", "Ok");
        }
    }
}
