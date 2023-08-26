using Moneybase.ViewModels;

namespace Moneybase.Pages.Startup;

public partial class OTPPage : ContentPage
{
	public OTPPage(OTPPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}