using Moneybase.Pages;

namespace Moneybase;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();
        MainPage = new LandingShell();
    }
}
