using CommunityToolkit.Mvvm.Input;
using Moneybase.Services;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

public partial class SecurityPINViewModel : ViewModelBase
{
    public SecurityPINViewModel(IApiRepository repo, IPopupNavigation popup)
    {
        repository = repo;
        mopupNavigation = popup;
        loadingPopup = new Pages.LoadingPopup();
    }

    [RelayCommand]
    public async Task CheckPin(string code)
    {
        //if response is true, it means the user's pin is correct. Move on
        await mopupNavigation.PushAsync(loadingPopup);
        bool response = await repository.CheckPIN(AuthenticationResult.UniqueId, code);
        if (response.Equals(true))
        {
            Application.Current.MainPage = new AppShell();
        }
        else 
        {
            await Shell.Current.DisplayAlert("Pin is incorrect", "Re-enter your pin", "Cancel");
        }
        await mopupNavigation.PopAsync();
    }
}
