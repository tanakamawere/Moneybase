using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages;
using Moneybase.Pages.RemotePayPages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;
using The49.Maui.BottomSheet;

namespace Moneybase.ViewModels
{
    public partial class CreateRemoteSessionViewModel : ViewModelBase
    {
        [ObservableProperty]
        string remoteSessionPhoneNumber;
        [ObservableProperty]
        string remoteSessionAmount;

        BottomSheet bottomSheet;

        public CreateRemoteSessionViewModel(IApiRepository repo, IPopupNavigation popup, BottomSheet _bottomSheet)
        {
            repository = repo;
            mopupNavigation = popup;
            bottomSheet = _bottomSheet;

            loadingPopup = new LoadingPopup();
        }

        [RelayCommand]
        async Task CreateRemoteSession()
        {
            try
            {
                await bottomSheet.DismissAsync();
                await mopupNavigation.PushAsync(loadingPopup);
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

                var response = await repository.CreateRemoteSession(remotePayClientDto);

                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Something is wrong: {ex}", "Okay");
            }
            finally 
            {
                await mopupNavigation.PopAsync(true);  
            }
        }
    }
}
