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

    public ProfileViewModel(IApiRepository repo, IPopupNavigation popup)
    {
        repository = repo;
        mopupNavigation = popup;
        loadingPopup = new LoadingPopup();
        GetUser();
    }

    [RelayCommand]
    private async Task GetUser()
    {
        await mopupNavigation.PushAsync(loadingPopup);
        try
        {
            User = await repository.GetUser(AuthenticationResult.UniqueId);
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
            await publicClientSingleton.SignOutAsync();

            await Task.Delay(3000);

            Application.Current.MainPage = new LandingShell();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            await mopupNavigation.PopAsync(true);
        }
    }
}
