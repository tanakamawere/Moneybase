using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages;
using Moneybase.Services;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    public bool isBusy;
    [ObservableProperty]
    public string title;

    [ObservableProperty]
    public string userPhoneNumber;

    public IPopupNavigation popups;

    public string userPhoneSecret;

    public IApiRepository repository;
    public IPopupNavigation mopupNavigation;
    public LoadingPopup loadingPopup;
    public NetworkAccess networkAccess;
    public ViewModelBase()
    {
        userPhoneSecret = "userPhoneSecret";

        string phoneNumber = SecureStorage.Default.GetAsync(userPhoneSecret).Result;
        if (!string.IsNullOrEmpty(phoneNumber))
            UserPhoneNumber = phoneNumber;
    }

    public bool CheckForInternet()
    {
        bool internet = false;
        if (networkAccess.Equals(NetworkAccess.Internet))
            internet = true;
        return internet;
    }
}
