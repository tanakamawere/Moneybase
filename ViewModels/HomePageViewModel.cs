using CommunityToolkit.Mvvm.ComponentModel;
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

    public HomePageViewModel(IApiRepository repo)
    {
        repository = repo;

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
}
