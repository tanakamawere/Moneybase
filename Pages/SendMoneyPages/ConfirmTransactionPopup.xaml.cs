using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Services;

namespace Moneybase.Pages.SendMoneyPages;

public partial class ConfirmTransactionPopup
{
	private readonly Transaction transaction;
	private readonly IApiRepository repository;
	public ConfirmTransactionPopup(Transaction trans, IApiRepository repo)
	{
		InitializeComponent();
		transaction = trans;
		repository = repo;

        transactionProvider.Text = $"Transaction Type: {transaction.TransactionProviders}";
        recPhoneNum.Text = $"To: {transaction.RecipientPhoneNum}";
		amount.Text = $"{transaction.Currency}$ {transaction.Amount}";
	}

    private void Cancel(object sender, EventArgs e)
    {
		MopupService.Instance.PopAsync();
    }

	private async void SendTransaction(object sender, EventArgs e)
	{
		await repository.PostTransaction(transaction);
	}
}