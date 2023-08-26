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
        await mopupNavigation.PushAsync(loadingPopup);
        try
        {
            User = await repository.GetUser(UserPhoneNumber);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            await mopupNavigation.PopAsync(true);
        }
    }


    [RelayCommand]
    private async Task SignOut()
    {
        await mopupNavigation.PushAsync(loadingPopup);
        try
        {
            SecureStorage.RemoveAll();
            App.Current.MainPage = new LandingShell();

            await mopupNavigation.PushAsync(logoutPopup, true);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
