using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages;
using Moneybase.Pages.RemotePayPages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

public partial class RemotePayViewModel : ViewModelBase
{
    [ObservableProperty]
    IEnumerable<RemotePay> remotePaySessions;

    public RemotePayViewModel(IApiRepository repo, IPopupNavigation popup)
    {
        repository = repo;
        mopupNavigation = popup;
        loadingPopup = new LoadingPopup();

        InitMethods();
    }

    [RelayCommand]
    async Task InitMethods()
    {
        RemotePaySessions = await repository.GetRemotePays(UserPhoneNumber);
    }

    [RelayCommand]
    async Task OpenCreateNewSession()
    {
        var createSessionSheet = new CreateRemotePayBottomSheet(repository, mopupNavigation);
        await createSessionSheet.ShowAsync();
    }

    [RelayCommand]
    async Task CancelRemoteSession(RemotePay remote)
    {
        if (remote == null) return;

        if (remote.IsActive.Equals(false) || remote.DateTimeExpired < DateTime.Now)
        {
            await Toast.Make("Remote session already is cancelled or has expired.", CommunityToolkit.Maui.Core.ToastDuration.Short, 12).Show();
        }
        else
        {
            string response = await Shell.Current.DisplayActionSheet("Cancel remote session?", "Dismiss", null, "Yes", "No");

            if (response.Equals("Yes"))
            {
                await mopupNavigation.PushAsync(loadingPopup);
                await repository.InactivateRemotePayment(remote);
                await mopupNavigation.PopAsync(true);
            }
        }
    }
}
