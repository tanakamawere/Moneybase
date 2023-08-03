using Moneybase.ViewModels;

namespace Moneybase.Pages.WalletPages;

public partial class CreateSavingsAccountPage : ContentPage
{
	public CreateSavingsAccountPage(CreateSavingsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}