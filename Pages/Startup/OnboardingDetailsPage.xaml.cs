using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class OnboardingDetailsPage : ContentPage
{
	public OnboardingDetailsPage(OnboardingDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}