using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages;
using Moneybase.Pages.Startup;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

public partial class AppLandingViewModel : ViewModelBase
{
    private readonly NoInternetPopup noInternetPopup;
    public AppLandingViewModel(IApiRepository repo, IPopupNavigation popup)
    {
        repository = repo;
        mopupNavigation = popup;
        loadingPopup = new LoadingPopup();
        noInternetPopup = new NoInternetPopup();
        networkAccess = Connectivity.Current.NetworkAccess;
    }

    [RelayCommand]
    async Task GoToPage()
    {
        await Shell.Current.GoToAsync(nameof(SecurityPINPage));
    }

    [RelayCommand]
    private async Task SignUp()
    {
        if (CheckForInternet().Equals(true))
        {
            await Shell.Current.GoToAsync(nameof(SignUpPage));
            //AuthenticationResult authenticationResult = await publicClientSingleton.AcquireTokenSilentAsync();

            //if (!authenticationResult.Account.Equals(null))
            //{
            //    FirstTimeUser userIsNew = await repository.UserIsNew(UserPhoneNumber);
            //    if (userIsNew.IsNew.Equals(false))
            //        await Shell.Current.GoToAsync(nameof(SecurityPINPage));
            //    else
            //    {
            //        string emailYangu = authenticationResult.ClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == "emails").Value.ToString();
            //        await Shell.Current.GoToAsync($"{nameof(OnboardingDetailsPage)}?onboardingEmail={emailYangu}");
            //    }
            //    await mopupNavigation.PopAsync(true);
            //}
            //else
            //{
            //    await Shell.Current.DisplayAlert("Authentication failed", "Please try again", "Cancel");
            //    await mopupNavigation.PopAsync(true);
            //}
        }
        else
            ShowNoInternetPopup();
    }

    [RelayCommand]
    private async Task LogIn()
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

    public async void CheckIfLoggedIn()
    {
        try
        {
            UserPhoneNumber = await SecureStorage.Default.GetAsync(userPhoneSecret);
            if (!string.IsNullOrEmpty(UserPhoneNumber)) 
            {
                await Shell.Current.GoToAsync($"{nameof(LoginPage)}?alreadyLoggedIn={UserPhoneNumber}");
            }
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", "There's been an error trying to automatically log you in. Please try again", "Ok");
        }
    }

    public void ShowNoInternetPopup() => mopupNavigation.PushAsync(noInternetPopup);
}
