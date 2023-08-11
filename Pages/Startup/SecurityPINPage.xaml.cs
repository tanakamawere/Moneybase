using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class SecurityPINPage
{
	public SecurityPINPage(SecurityPINViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}