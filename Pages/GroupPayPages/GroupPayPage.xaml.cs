using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class GroupPayPage : ContentPage
{
	public GroupPayPage(GroupPayViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}