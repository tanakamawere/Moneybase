using Moneybase.ViewModels;

namespace Moneybase.Pages.Startup;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}