using MoneybaseLibrary.Models;
using Mopups.Services;

namespace Moneybase.Pages.SendMoneyPages;

public partial class ConfirmTransactionPopup
{
	public Transaction something;
	public ConfirmTransactionPopup(Transaction transaction)
	{
		InitializeComponent();

		MyLabel.Text = transaction.RecipientAccNum;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		MopupService.Instance.PopAsync();
    }
}