using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}