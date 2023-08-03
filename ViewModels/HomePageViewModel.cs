using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using Moneybase.MSALClient;
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
    string userName;
    [ObservableProperty]
    string email;


    public HomePageViewModel(IApiRepository repo)
    {
        repository = repo;
        //GetUser();
    }

    async void GetUser()
    {
        try
        {
            User = await repository.GetUser(AuthenticationResult.UniqueId);
            UserAccounts = User.Accounts.ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //private async Task FirstTimeDetailEntry()
    //{
    //    string authId = authenticationResult.UniqueId;
    //    string userIsNew = repository.UserIsNew(authId).Result;
    //    if (userIsNew.Equals(true))
    //        await Shell.Current.DisplayAlert("Hey there", "You are new here", "Coool");
        
    //}

    //Function Navigations
    [RelayCommand]
    private async Task NavigateTo(string page)
    {
        await Shell.Current.GoToAsync(page);
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
