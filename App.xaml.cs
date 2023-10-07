using Moneybase.Helpers;

namespace Moneybase;

public partial class App : Application
{
    public App()
	{
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Constants.SyncfusionLicense());
        InitializeComponent();
        MainPage = new LandingShell();
    }
}
