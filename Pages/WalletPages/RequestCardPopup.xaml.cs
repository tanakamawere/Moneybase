using CommunityToolkit.Maui.Alerts;
using Moneybase.Services;
using Mopups.Services;

namespace Moneybase.Pages.WalletPages;

public partial class RequestCardPopup
{
	private IApiRepository repository;
    LoadingPopup loadingPopup;
    private string UserId;
    public RequestCardPopup(IApiRepository repo, string userId)
	{
		InitializeComponent();
		repository = repo;
        UserId = userId;
        loadingPopup = new LoadingPopup();
    }

	private async void CreateCard(object sender, EventArgs e)
	{
        await MopupService.Instance.PushAsync(loadingPopup);
        bool result = await repository.CreateCard(UserId);
        if (result.Equals(true))
        {
            await Toast.Make("Card created successfully", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
        }
        else
        {
            await Toast.Make("Error creating card. Please contact us.", CommunityToolkit.Maui.Core.ToastDuration.Long, 12).Show(new CancellationToken());
        }

        await MopupService.Instance.PopAllAsync();
    }
	private async void Dismiss(object sender, EventArgs e) =>
		await MopupService.Instance.PopAsync();
}