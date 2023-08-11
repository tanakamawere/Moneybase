using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class SetPINPage : ContentPage
{
	public SetPINPage(SetPINViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}