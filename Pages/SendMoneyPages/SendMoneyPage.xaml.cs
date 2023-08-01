using Moneybase.ViewModels;

namespace Moneybase.Pages.SendMoneyPages;

public partial class SendMoneyPage : ContentPage
{
	public SendMoneyPage(SendMoneyViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}