using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class DirectPayPage : ContentPage
{
	public DirectPayPage(DirectPayViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}