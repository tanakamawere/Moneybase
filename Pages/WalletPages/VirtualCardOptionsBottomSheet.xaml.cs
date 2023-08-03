using CommunityToolkit.Maui.Alerts;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Services;

namespace Moneybase.Pages.WalletPages;

public partial class VirtualCardOptionsBottomSheet
{
	private IApiRepository repository;
    private VirtualCard Card { get; }

    LoadingPopup loadingPopup;
    public VirtualCardOptionsBottomSheet(IApiRepository rep, VirtualCard card)
	{
		InitializeComponent();
        Card = card;
        repository = rep;
        loadingPopup = new LoadingPopup();

        BindingContext = this;
    }

    private async void ViewCVV(object sender, TappedEventArgs e)
    {
        await Shell.Current.DisplayAlert("Virtual Card CVV", Card.CVV, "Ok");
    }
    private async void BlockUnblockCard(object sender, TappedEventArgs e)
    {
        await DismissAsync();
        await MopupService.Instance.PushAsync(loadingPopup);
        bool result = await repository.ChangeCardBlockStatus(Card.Id);
        if (result.Equals(true))
        {
            await Toast.Make("Card blocked/unbloced successfully", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
        }
        else
        {
            await Toast.Make("Error blocking/unblocking card. Please contact us.", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
        }
        await MopupService.Instance.PopAsync();
    }
    private async void DeleteCard(object sender, TappedEventArgs e)
    {
        string response = await Shell.Current.DisplayActionSheet("Do you really want to delete this card?", "Cancel", null, "OK");
        if (response == "OK")
        {
            await DismissAsync();
            await MopupService.Instance.PushAsync(loadingPopup);
            bool result = await repository.DeleteCard(Card.Id);
            if (result.Equals(true))
            {
                await Toast.Make("Card deleted successfully", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
            }
            else
            {
                await Toast.Make("Error deleting card. Please contact us.", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
            }

            await MopupService.Instance.PopAsync();
        }
    }
}