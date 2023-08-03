using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class WalletPage : ContentPage
{
	public WalletPage(WalletViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}