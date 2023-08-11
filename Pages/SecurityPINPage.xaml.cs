using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class SecurityPINPage : ContentPage
{
	public SecurityPINPage(SecurityPINViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}