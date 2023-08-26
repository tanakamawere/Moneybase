namespace Moneybase;

public partial class App : Application
{
    public App()
	{
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NGaF1cWGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEZjUX1YcHZRRmBcUkFwWA==");
        InitializeComponent();
        MainPage = new LandingShell();
    }
}
