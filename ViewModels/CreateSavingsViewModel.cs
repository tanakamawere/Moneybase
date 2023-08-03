using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

public partial class CreateSavingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private IEnumerable<Currencies> currenciesList;
    [ObservableProperty]
    private Currencies currencySelected = Currencies.USD;
    [ObservableProperty]
    private string savingsAccountTitle;
    [ObservableProperty]
    private Currencies savingsAccountCurrency;
    [ObservableProperty]
    private DateTime goalDueDate;
    [ObservableProperty]
    private Color selectedColour = Colors.LightSalmon;
    [ObservableProperty]
    private List<Color> colours;

    LoadingPopup loadingPopup = new();

    public CreateSavingsViewModel(IApiRepository repo, IPopupNavigation pops)
    {
        repository = repo;
        popups = pops;
        currenciesList = Enum.GetValues(typeof(Currencies)).Cast<Currencies>();
        Colours = new List<Color>
        {
            Colors.LightGreen,
            Colors.LightPink,
            Colors.LightBlue,
            Colors.LightSalmon
        };
    }

    [RelayCommand]
    private async Task CreateAccount()
    {
        await popups.PushAsync(loadingPopup);
        Account account = new()
        {
            AccountType = AccountType.Saving,
            AccountName = SavingsAccountTitle,
            Currency = SavingsAccountCurrency,
            DueDate = GoalDueDate,
            Colour = SelectedColour.ToString(),
            Balance = decimal.Zero,
        };

        bool result = await repository.CreateSavingsAccount(account, authenticationResult.UniqueId);
        if (result.Equals(true))
        {
            await Toast.Make("Account created successfully", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
        }
        else
        {
            await Toast.Make("Error creating account. Please contact us.", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
        }

        await popups.PopAsync();
    }
}
