using Moneybase.Pages;
using Moneybase.Pages.Startup;

namespace Moneybase;

public partial class LandingShell : Shell
{
	public LandingShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(AppLandingPage), typeof(AppLandingPage));
		Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
        Routing.RegisterRoute(nameof(SecurityPINPage), typeof(SecurityPINPage));
        Routing.RegisterRoute(nameof(SetPINPage), typeof(SetPINPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(OTPPage), typeof(OTPPage));
    }
}