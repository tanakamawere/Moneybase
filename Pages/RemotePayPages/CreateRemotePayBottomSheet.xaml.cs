using Moneybase.Services;
using Moneybase.ViewModels;
using Mopups.Interfaces;

namespace Moneybase.Pages.RemotePayPages;

public partial class CreateRemotePayBottomSheet
{
    public CreateRemotePayBottomSheet(IApiRepository repo, IPopupNavigation popupNavigation)
	{
		InitializeComponent();
		BindingContext = new CreateRemoteSessionViewModel(repo, popupNavigation, this);
	}
    public async void CloseSheet()
    {
        await DismissAsync();
    }
}