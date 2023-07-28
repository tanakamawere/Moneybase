using CommunityToolkit.Maui.Alerts;
using Microsoft.Identity.Client;
using Microsoft.Maui.ApplicationModel.Communication;
using Moneybase.MSALClient;
using Moneybase.Services;
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
        viewModel.CheckIfLoggedIn();
    }
}