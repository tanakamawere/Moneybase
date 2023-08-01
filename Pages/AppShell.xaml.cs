using Moneybase.Pages;
using Moneybase.Pages.SendMoneyPages;

namespace Moneybase;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(WalletPage), typeof(WalletPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));


        Routing.RegisterRoute(nameof(SendMoneyPage), typeof(SendMoneyPage));
        Routing.RegisterRoute(nameof(GroupPayPage), typeof(GroupPayPage));
        Routing.RegisterRoute(nameof(DirectPayPage), typeof(DirectPayPage));
        Routing.RegisterRoute(nameof(RemotePayPage), typeof(RemotePayPage));
        Routing.RegisterRoute(nameof(CashOutPage), typeof(CashOutPage));
    }
}
