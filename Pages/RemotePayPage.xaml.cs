using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class RemotePayPage : ContentPage
{
	public RemotePayPage(RemotePayViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}