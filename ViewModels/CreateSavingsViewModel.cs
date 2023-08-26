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
    private decimal goalAmount;
    [ObservableProperty]
    private Color selectedColour = Colors.LightSalmon;
    [ObservableProperty]
    private List<Color> colours;
    [ObservableProperty]
    private bool isGeneralSavings = true;
    [ObservableProperty]
    private string borderDescription = "Great for earning interest on the money you save.";
    [ObservableProperty]
    private string borderTitle = "General Savings Account";

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
        Account account;
        if (IsGeneralSavings.Equals(true))
        {
            account = new()
            {
                AccountType = AccountType.Saving,
                AccountName = "General",
                Currency = SavingsAccountCurrency,
                DueDate = DateTime.Now.AddYears(50),
                Colour = Colors.LightBlue.ToHex().ToString(),
                GoalAmount = decimal.Zero,
                SavingsType = SavingsType.General,
                Balance = decimal.Zero,
            };
        }
        else
        {
            account = new()
            {
                AccountType = AccountType.Saving,
                AccountName = SavingsAccountTitle,
                Currency = SavingsAccountCurrency,
                DueDate = GoalDueDate,
                Colour = SelectedColour.ToHex().ToString(),
                GoalAmount = GoalAmount,
                SavingsType = SavingsType.Goal,
                Balance = decimal.Zero,
            };
        }

        bool result = await repository.CreateSavingsAccount(account, UserPhoneNumber);
        if (result.Equals(true))
        {
            await Toast.Make("Account created successfully", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
        }
        else
        {
            await Toast.Make("Error creating account. Please contact us.", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
        }

        await Shell.Current.GoToAsync("../");
        await popups.PopAsync();
    }
    partial void OnIsGeneralSavingsChanged(bool oldValue, bool newValue)
    {
        if (newValue.Equals(false))
        {
            BorderTitle = "Goals Account";
            BorderDescription = "Perfect for working towards a particular special day, be it a birthday or anything you like";
        }
        else
        {
            BorderTitle = "General Savings Account";
            BorderDescription = "Great for earning interest on the money you save.";
        }
    }

    [RelayCommand]
    private void ChangeAccountStatusType()
    {
        IsGeneralSavings = !IsGeneralSavings;
    }
}
