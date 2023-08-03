using CommunityToolkit.Mvvm.ComponentModel;
using Moneybase.Services;
using MoneybaseLibrary.Models;

namespace Moneybase.ViewModels;

public partial class WalletViewModel : ViewModelBase
{
    [ObservableProperty]
    private IEnumerable<VirtualCard> userCards;

    public WalletViewModel(IApiRepository repo)
    {
        repository = repo;
        GetVirtualCards();
    }

    private async Task GetVirtualCards()
    {
        UserCards = await repository.GetVirtualCards(AuthenticationResult.UniqueId);
    }
}
