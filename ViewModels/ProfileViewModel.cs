using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

public partial class ProfileViewModel : ViewModelBase
{
    [ObservableProperty]
    private User user;
    private readonly LogoutPopup logoutPopup;

    public ProfileViewModel(IApiRepository repo, IPopupNavigation popup)
    {
        repository = repo;
        mopupNavigation = popup;
        loadingPopup = new LoadingPopup();
        logoutPopup = new LogoutPopup();

        GetUser();
    }

    [RelayCommand]
    private async Task GetUser()
    {
        try
        {
            User = await repository.GetUserAsync(UserPhoneNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }


    [RelayCommand]
    private async Task SignOut()
    {
        try
        {
            SecureStorage.RemoveAll();
            Application.Current.MainPage = new LandingShell();

            await mopupNavigation.PushAsync(logoutPopup, true);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
