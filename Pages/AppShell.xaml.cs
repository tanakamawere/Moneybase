using Moneybase.Pages;

namespace Moneybase;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(StatisticsPage), typeof(StatisticsPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
    }
}
