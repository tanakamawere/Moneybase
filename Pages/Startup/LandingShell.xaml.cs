using Moneybase.Pages;

namespace Moneybase;

public partial class LandingShell : Shell
{
	public LandingShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(AppLandingPage), typeof(AppLandingPage));
		Routing.RegisterRoute(nameof(OnboardingDetailsPage), typeof(OnboardingDetailsPage));
        Routing.RegisterRoute(nameof(SecurityPINPage), typeof(SecurityPINPage));
        Routing.RegisterRoute(nameof(SetPINPage), typeof(SetPINPage));
    }
}