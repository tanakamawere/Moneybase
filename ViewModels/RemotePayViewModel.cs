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
}
