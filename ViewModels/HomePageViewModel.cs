using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using Moneybase.MSALClient;
using Moneybase.Pages;
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

    private readonly IApiRepository repository;

    private AuthenticationResult authenticationResult;
    private PublicClientSingleton publicClientSingleton;

    [ObservableProperty]
    string[] accessTokenScopes = new string[] { "No scopes found in access token" };
    [ObservableProperty]
    string userName;
    [ObservableProperty]
    string email;

    public HomePageViewModel(IApiRepository repo)
    {
        repository = repo;

        publicClientSingleton = new PublicClientSingleton();

        //authenticationResult = publicClientSingleton.CheckIfUserAlreadyLoggedIn(Constants.Scopes).Result;
        //GetUser();
    }

    async void GetUser()
    {
        try
        {
            User = await repository.GetUser(authenticationResult.UniqueId);
            UserAccounts = await repository.GetAccountsAsync(authenticationResult.UniqueId);
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

    [RelayCommand]
    private async Task SignOut()
    {
        await publicClientSingleton.SignOutAsync().ContinueWith((t) =>
        {
            return Task.CompletedTask;
        });

        Application.Current.MainPage = new LandingShell();
    }


    //Function Navigations
    [RelayCommand]
    private async Task NavigateTo(string page)
    {
        await Shell.Current.GoToAsync(page);
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
