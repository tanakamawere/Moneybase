using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages.SendMoneyPages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;
using System.ComponentModel;

namespace Moneybase.ViewModels;

[QueryProperty(nameof(CurrencyToSend), "currencyToSend")]
public partial class SendMoneyViewModel : ViewModelBase
{
    [ObservableProperty]
    private string currencyToSend;
    [ObservableProperty]
    private Transaction transaction;
    [ObservableProperty]
    private IEnumerable<TransactionProvider> transactionProviders;
    [ObservableProperty]
    private IEnumerable<Banks> banks;
    [ObservableProperty]
    private TransactionProvider provider;
    [ObservableProperty]
    private Banks bank;
    [ObservableProperty]
    private bool banksVisible = false;
    [ObservableProperty]
    private bool accountEntryVisible = false;
    [ObservableProperty]
    private bool moneyBaseUserPreview = false;

    [ObservableProperty]
    private string recAccNum;
    [ObservableProperty]
    private string recPhoneNum;
    [ObservableProperty]
    private decimal amount;

    private readonly IApiRepository repository;
    private readonly IPopupNavigation popups;

    public SendMoneyViewModel(IApiRepository repo, IPopupNavigation popups)
    {
        repository = repo;
        this.popups = popups;
        Title = $"Send {CurrencyToSend}";

        transactionProviders = Enum.GetValues(typeof(TransactionProvider)).Cast<TransactionProvider>();
        banks = Enum.GetValues(typeof(Banks)).Cast<Banks>();
    }

    partial void OnRecPhoneNumChanged(string oldValue, string newValue)
    {
        if (Provider.Equals(TransactionProvider.MoneybaseTransfer))
        {
            //logic to get the user from the api
        }
    }

    partial void OnProviderChanged(TransactionProvider oldValue, TransactionProvider newValue)
    {
        switch (newValue)
        {
            case TransactionProvider.MoneybaseTransfer:
                AccountEntryVisible = false;
                BanksVisible = false;
                break;
            case TransactionProvider.Zipit:
                AccountEntryVisible = true;
                MoneyBaseUserPreview = false;
                BanksVisible = true;
                break;
            case TransactionProvider.Ecocash:
            case TransactionProvider.Unregistered:
            case TransactionProvider.InnBucks:
                AccountEntryVisible = false;
                MoneyBaseUserPreview = false;
                BanksVisible = false;
                break;
            default:
                break;
        }
    }

    [RelayCommand]
    private async Task SubmitTransaction()
    {
        Transaction = new()
        {
            TransactionType = TransactionType.Transfer,
            Image = "https://somewhere.com",
            SenderAccNum = "Get This Somehow",
            TransactionProviders = Provider,
            Currency = (Currencies)Enum.Parse(typeof(Currencies), CurrencyToSend),
            RecipientName = Provider.Equals(TransactionProvider.MoneybaseTransfer) ? "Self" : Bank.ToString(),
            Amount = Amount,
            RecipientAccNum = RecAccNum,
            RecipientPhoneNum = RecPhoneNum,
        };

        await popups.PushAsync(new ConfirmTransactionPopup(Transaction, repository));
    }
}
