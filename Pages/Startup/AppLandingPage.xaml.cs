using Moneybase.ViewModels;
namespace Moneybase.Pages;

public partial class AppLandingPage : ContentPage
{
    private AppLandingViewModel viewModel => BindingContext as AppLandingViewModel;

    public AppLandingPage(AppLandingViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (viewModel.CheckForInternet().Equals(true))
        {
            viewModel.CheckIfLoggedIn();
        }
        else 
            viewModel.ShowNoInternetPopup();
    }
}