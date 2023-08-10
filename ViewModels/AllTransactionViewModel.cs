using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;
using Mopups.Services;

namespace Moneybase.ViewModels;

public partial class AllTransactionViewModel : ViewModelBase
{
    [ObservableProperty]
    IEnumerable<Transaction> transactions;

    public AllTransactionViewModel(IApiRepository api, IPopupNavigation navigation)
    {
        repository = api;
        mopupNavigation = navigation;

        GetTransactions();
    }
    [RelayCommand]
    async Task GetTransactions(int num = 10) 
    {
        Transactions = await repository.GetTransactionsAsync(AuthenticationResult.UniqueId, num);
    }
}
