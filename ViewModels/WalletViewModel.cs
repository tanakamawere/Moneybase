using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages;
using Moneybase.Pages.WalletPages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;
using Mopups.Services;

namespace Moneybase.ViewModels;

public partial class WalletViewModel : ViewModelBase
{
    [ObservableProperty]
    private IEnumerable<VirtualCard> userCards;
    [ObservableProperty]
    private IEnumerable<Account> savingsAccounts;

    RequestCardPopup requestCardPopup;

    public WalletViewModel(IApiRepository repo, IPopupNavigation popup)
    {
        repository = repo;
        mopupNavigation = popup;
        loadingPopup = new LoadingPopup();
        requestCardPopup = new RequestCardPopup(repository, UserPhoneNumber);
        InitMethods();
    }
    [RelayCommand]
    private async Task InitMethods()
    {
        await mopupNavigation.PushAsync(loadingPopup);
        try
        {
            await GetVirtualCards();
            await GetSavingsAccounts();
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

    private async Task GetVirtualCards()
    {
        try
        {
            UserCards = await repository.GetVirtualCards(UserPhoneNumber);
        }
        catch (Exception)
        {
            //Handle exception
            throw;
        }
    }
    private async Task GetSavingsAccounts()
    {
        try
        {
            SavingsAccounts = await repository.GetSavingsAccounts(UserPhoneNumber);
        }
        catch (Exception)
        { 
            throw;
        }
    }

    [RelayCommand]
    private async Task RequestVirtualCard()
    {
        await MopupService.Instance.PushAsync(requestCardPopup);
    }

    [RelayCommand]
    private async Task GoToSavingsAccount(Account account)
    {
        if(account == null) { return; }

        var send = new ViewSavingBottomSheet(repository, account);
        await send.ShowAsync();
    }

    [RelayCommand]
    private async Task OpenCreateSavingsPage()
    {
        await Shell.Current.GoToAsync(nameof(CreateSavingsAccountPage));
    }

    [RelayCommand]
    private async Task OpenCardOptions(VirtualCard card)
    {
        if (card == null)
            return;

        var send = new VirtualCardOptionsBottomSheet(repository, card);
        await send.ShowAsync();
    }
}
