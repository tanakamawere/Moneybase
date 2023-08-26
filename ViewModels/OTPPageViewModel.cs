using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;

namespace Moneybase.ViewModels
{
    [QueryProperty(nameof(User), "userToCreate")]
    [QueryProperty(nameof(OTP), "otpGenerated")]
    public partial class OTPPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string oTP;
        [ObservableProperty]
        private string userEnteredOTP;
        [ObservableProperty]
        private User user;

        public OTPPageViewModel(IApiRepository repo, IPopupNavigation popup)
        {
            repository = repo;
            mopupNavigation = popup;
            loadingPopup = new LoadingPopup();
        }

        [RelayCommand]
        private async Task ConfirmOtp()
        {
            if (UserEnteredOTP == null) { return; }


            await mopupNavigation.PushAsync(loadingPopup);
            if (UserEnteredOTP.Equals(OTP))
            {
                await Shell.Current.GoToAsync(nameof(SetPINPage), true, new Dictionary<string, object>
                {
                    {"onboardingUser", User},
                });
            }
            else
            {
                await Shell.Current.DisplayAlert("Incorrect OTP", "That wasn't quite right. Try again.", "Okay");
            }
            await mopupNavigation.PopAsync();
        }
    }
}
