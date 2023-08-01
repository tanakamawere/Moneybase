using MoneybaseLibrary.Models;
using The49.Maui.BottomSheet;

namespace Moneybase.Pages.SendMoneyPages;

public partial class SendCurrencyBottomSheet : BottomSheet
{
	public SendCurrencyBottomSheet()
	{
		InitializeComponent();
	}

    private async void USDTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SendMoneyPage)}?currencyToSend={Currencies.USD}");
        await DismissAsync();
    }

    private async void ZWLTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SendMoneyPage)}?currencyToSend={Currencies.ZWL}");
        await DismissAsync();
    }
}