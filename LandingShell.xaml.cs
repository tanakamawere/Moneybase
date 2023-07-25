using Moneybase.Pages;

namespace Moneybase;

public partial class LandingShell : Shell
{
	public LandingShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(AppLandingPage), typeof(AppLandingPage));
		Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
	}
}