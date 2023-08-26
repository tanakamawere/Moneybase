using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Services;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

[QueryProperty(nameof(UserPhoneNumber), "alreadyLoggedIn")]
public partial class LoginViewModel : ViewModelBase
{
    [ObservableProperty]
    private string pIN;

    public LoginViewModel(IApiRepository repo, IPopupNavigation popup)
    {
        repository = repo;
        mopupNavigation = popup;
        loadingPopup = new Pages.LoadingPopup();
    }


    [RelayCommand]
    public async Task Login()
    {
        //if response is true, it means the user's pin is correct. Move on
        await mopupNavigation.PushAsync(loadingPopup);
        try
        {
            bool response = await repository.CheckPIN(UserPhoneNumber, PIN);
            if (response.Equals(true))
            {
                //If there is a number in the secure storage, it means the user is already logged in
                await SecureStorage.Default.SetAsync(userPhoneSecret, UserPhoneNumber);

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
