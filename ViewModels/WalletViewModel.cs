﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages.WalletPages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Services;

namespace Moneybase.ViewModels;

public partial class WalletViewModel : ViewModelBase
{
    [ObservableProperty]
    private IEnumerable<VirtualCard> userCards;
    [ObservableProperty]
    private IEnumerable<Account> savingsAccounts;

    RequestCardPopup requestCardPopup;

    public WalletViewModel(IApiRepository repo)
    {
        repository = repo;
        requestCardPopup = new RequestCardPopup(repository, AuthenticationResult.UniqueId);
        //requestCardPopup = new RequestCardPopup(repository, "zvandoda");
        InitMethods();
    }
    [RelayCommand]
    private async Task InitMethods()
    {
        await GetVirtualCards();
        await GetSavingsAccounts();
    }

    private async Task GetVirtualCards()
    {
        UserCards = await repository.GetVirtualCards(AuthenticationResult.UniqueId);
    }
    private async Task GetSavingsAccounts()
    {
        SavingsAccounts = await repository.GetSavingsAccounts(AuthenticationResult.UniqueId);
    }

    [RelayCommand]
    private async Task RequestVirtualCard()
    {
        await MopupService.Instance.PushAsync(requestCardPopup);
        //await GetVirtualCards();
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

        //await GetVirtualCards();
    }
}
