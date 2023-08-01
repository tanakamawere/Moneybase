using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class CashOutPage : ContentPage
{
	public CashOutPage(CashOutViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}