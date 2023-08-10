using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfileViewModel profileView)
	{
		InitializeComponent();
		BindingContext = profileView;
	}
}