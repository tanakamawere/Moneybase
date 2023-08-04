using CommunityToolkit.Maui.Alerts;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Services;
using ZXing;

namespace Moneybase.Pages.WalletPages;

public partial class ViewSavingBottomSheet
{
	private Account account;
	private readonly IApiRepository repository;
    LoadingPopup loadingPopup;
    public ViewSavingBottomSheet(IApiRepository repo, Account acc)
	{
		InitializeComponent();
		BindingContext = this;
		repository = repo;
		account = acc;
        loadingPopup = new LoadingPopup();

        //assignments

        mainBorder.BackgroundColor = Color.Parse(account.Colour);
		accountTitle.Text = account.AccountName;
		accountCurrency.Text = account.Currency.ToString();
		accountBalance.Text = account.Balance.ToString();

        if (!account.GoalAmount.Equals(decimal.Zero))
        {
            goalProgress.IsVisible = true;
            goalProgress.Progress = (double)(account.Balance / account.GoalAmount);
        }

		accountNumber.Text = account.AccountNumber.ToString();
		accountSavingsType.Text = account.SavingsType.ToString();
		accountGoalBalance.Text = account.GoalAmount.ToString();
		accountDateCreated.Text = $"Created on: {account.DateCreated}";
		accountGoalDueDate.Text = account.DueDate.ToShortDateString();
	}

    private void DepositTapped(object sender, TappedEventArgs e)
    {

    }
    private void WithdrawTapped(object sender, TappedEventArgs e)
    {

    }
    private async void DeleteTapped(object sender, TappedEventArgs e)
    {
        await DismissAsync();
        await MopupService.Instance.PushAsync(loadingPopup);
        if (account.Balance > 0)
			await Toast.Make("There is money in this account. First withdraw from the account.", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
		else
		{
            bool result = await repository.DeleteSavingsAccount(account.Id);
            if (result.Equals(true))
            {
                await Toast.Make("Deleted successfully", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
            }
            else
            {
                await Toast.Make("Error deleting account. Please contact us.", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
            }
        }
        await MopupService.Instance.PopAsync();
    }
}