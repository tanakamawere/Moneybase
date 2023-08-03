using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using Moneybase.MSALClient;
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
    private readonly PublicClientSingleton publicClientSingleton;


    public ViewModelBase()
    { 
        publicClientSingleton = new PublicClientSingleton();
        //authenticationResult = publicClientSingleton.CheckIfUserAlreadyLoggedIn(Constants.Scopes).Result;
    }

    [RelayCommand]
    private async Task SignOut()
    {
        await publicClientSingleton.SignOutAsync().ContinueWith((t) =>
        {
            return Task.CompletedTask;
        });

        Application.Current.MainPage = new LandingShell();
    }
}
