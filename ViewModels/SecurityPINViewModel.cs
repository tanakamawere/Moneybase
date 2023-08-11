using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

[QueryProperty(nameof(User), "onboardingUser")]
public partial class SecurityPINViewModel : ViewModelBase
{
    [ObservableProperty]
    string pIN;
    [ObservableProperty]
    User user;

    public SecurityPINViewModel(IApiRepository repo, IPopupNavigation popup)
    {
        repository = repo;
        mopupNavigation = popup;
        loadingPopup = new Pages.LoadingPopup();
    }

    [RelayCommand]
    async Task PostUserWithPin()
    {
        await mopupNavigation.PushAsync(loadingPopup);
        if(string.IsNullOrEmpty(PIN)) 
        {
            await mopupNavigation.PopAsync();
            await Toast.Make("PIN can't be empty. Provide one", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
            return; 
        }
        User.PIN = PIN;
        var result = await repository.PostUser(User);

        if (result.Equals(true))
        {
            await Toast.Make("User created successfully", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await Toast.Make("Error creating user. Please contact us.", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
        }

        await mopupNavigation.PopAsync();
    }
}
