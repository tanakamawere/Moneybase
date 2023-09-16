using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages;
using Moneybase.Pages.DirectPayPages;
using Moneybase.Pages.SendMoneyPages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

public partial class HomePageViewModel : ViewModelBase
{
    [ObservableProperty]
    User user;
    [ObservableProperty]
    IEnumerable<Account> userAccounts;
    [ObservableProperty]
    IEnumerable<Transaction> userTransactions;
    [ObservableProperty]
    string userName;
    [ObservableProperty]
    string email;

    public HomePageViewModel(IApiRepository repo, IPopupNavigation popup)
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
            User = await repository.GetUserWithCurrentAccountsAsync(UserPhoneNumber);
            UserAccounts = User.Accounts.Where(x => x.AccountType != AccountType.Saving).ToList();
            UserTransactions = await repository.GetTransactionsAsync(UserPhoneNumber, 3);
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

    //private async Task FirstTimeDetailEntry()
    //{
    //    string authId = UserPhoneNumber;
    //    string userIsNew = repository.UserIsNew(authId).Result;
    //    if (userIsNew.Equals(true))
    //        await Shell.Current.DisplayAlert("Hey there", "You are new here", "Coool");
        
    //}

    //Function Navigations
    [RelayCommand]
    private async Task NavigateTo(string page)
    {
        await mopupNavigation.PushAsync(loadingPopup);
        await Shell.Current.GoToAsync(page);
        await mopupNavigation.PopAsync(true);
    }

    [RelayCommand]
    private async Task DirectPayOptions()
    {
        var directPaySheet = new PayOptionsSheet();
        await directPaySheet.ShowAsync();
    }

    [RelayCommand]
    private async Task SendMoney()
    {
        var send = new SendCurrencyBottomSheet();
        await send.ShowAsync();
    }

    [RelayCommand]
    private async Task OpenReceiveSheet()
    {
        var sheetSomething = new ReceiveMoneySheet();
        await sheetSomething.ShowAsync();
    }
}
