using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using Moneybase.MSALClient;
using Moneybase.Services;
using MoneybaseLibrary.Models;

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

        authenticationResult = publicClientSingleton.CheckIfUserAlreadyLoggedIn(Constants.Scopes).Result;
        GetUser();
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
    private async Task TestApi()
    {
        FirstTimeUser userIsNew = await repository.UserIsNew(authenticationResult.UniqueId);
        await Shell.Current.DisplayAlert("Hey there", userIsNew.IsNew.ToString(), "Coool");
    }

    [RelayCommand]
    private async Task SignOut()
    {
        await publicClientSingleton.SignOutAsync().ContinueWith((t) =>
        {
            return Task.CompletedTask;
        });

        Application.Current.MainPage = new LandingShell();
    }
}
