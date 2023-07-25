using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.MSALClient;
using Moneybase.Pages;
using Moneybase.Services;
using MoneybaseLibrary.Models;

namespace Moneybase.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    [ObservableProperty]
    User user;
    [ObservableProperty]
    IEnumerable<Account> userAccounts;

    private readonly IApiRepository repository;
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

        GetUser();
    }

    async void GetUser()
    {
        try
        {
            User = await repository.GetUser(1);
            UserAccounts = await repository.GetAccountsAsync(1);
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
