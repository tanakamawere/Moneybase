using CommunityToolkit.Mvvm.Input;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;
using System.Net.NetworkInformation;
using Twilio.Types;

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
    public async Task CheckPin(string PIN)
    {

        //if response is true, it means the user's pin is correct. Move on
        await mopupNavigation.PushAsync(loadingPopup);
        try
        {
            AuthUser authUser = new() 
            {
                PhoneNumber = UserPhoneNumber,
                PIN = PIN
            };
            var response = await repository.LoginUserAsync(authUser);

            string somehing = "1";

            if (response.Equals(true))
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await Shell.Current.DisplayAlert("Pin is incorrect", "Re-enter your pin", "Cancel");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Account not found", ex.Message, "Ok");
        }
        await mopupNavigation.PopAsync();
    }
}
