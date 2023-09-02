using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;

namespace Moneybase.ViewModels
{
    public partial class CreateRemoteSessionViewModel : ViewModelBase
    {
        [ObservableProperty]
        string remoteSessionPhoneNumber;
        [ObservableProperty]
        string remoteSessionAmount;

        public CreateRemoteSessionViewModel(IApiRepository repo, IPopupNavigation popup)
        {
            repository = repo;
            mopupNavigation = popup;
            loadingPopup = new LoadingPopup();
        }

        [RelayCommand]
        async Task CreateRemoteSession()
        {
            try
            {
                RemotePayClientSideDto remotePayClientDto = new()
                {
                    phoneNumberUserToPay = RemoteSessionPhoneNumber,
                    phoneNumberUserWithMoney = UserPhoneNumber,
                };

                RemotePay remotePay = new()
                {
                    Amount = decimal.Parse(RemoteSessionAmount),
                };
                remotePayClientDto.RemotePay = remotePay;

                await repository.CreateRemoteSession(remotePayClientDto);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Something is wrong: {ex}", "Okay");
            }
        }
    }
}
