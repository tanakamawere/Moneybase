using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Identity.Client;
using Moneybase.MSALClient;
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
    public AuthenticationResult authenticationResult;
    public IPopupNavigation popups;

    public IApiRepository repository;
    public IPopupNavigation mopupNavigation;
    public readonly PublicClientSingleton publicClientSingleton;
    public LoadingPopup loadingPopup;

    public ViewModelBase()
    { 
        publicClientSingleton = new PublicClientSingleton();

        try
        {
            authenticationResult = publicClientSingleton.CheckIfUserAlreadyLoggedIn(Constants.Scopes).Result;
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Oops...", "That wasn't supposed to happen. Please restart the app.", "Ok");
        }
    }
}
