using Moneybase.Services;
using Moneybase.ViewModels;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;

namespace Moneybase.Pages.RemotePayPages;

public partial class CreateRemotePayBottomSheet
{
	private readonly IApiRepository repository;
    private readonly string UserPhoneNumber;
    LoadingPopup loadingPopup;
    public CreateRemotePayBottomSheet(IApiRepository repo, IPopupNavigation popupNavigation)
	{
		InitializeComponent();
		BindingContext = new CreateRemoteSessionViewModel(repo, popupNavigation);
        loadingPopup = new LoadingPopup();
	}

    private async void CreateRemoteSession(object sender, EventArgs e)
    {

        RemotePayClientSideDto remotePayClient = new()
        {
            phoneNumberUserToPay = createRSPhoneNumber.Text.ToString(),
            phoneNumberUserWithMoney = UserPhoneNumber,
        };
        remotePayClient.RemotePay.Amount = decimal.Parse(createRSAmount.Text.ToString());

        await repository.CreateRemoteSession(remotePayClient);
    }
}