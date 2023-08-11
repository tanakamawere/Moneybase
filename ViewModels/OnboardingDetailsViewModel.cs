using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

[QueryProperty(nameof(Email), "onboardingEmail")]
public partial class OnboardingDetailsViewModel : ViewModelBase
{
    [ObservableProperty]
    string name;
    [ObservableProperty]
    string phoneNumber;
    [ObservableProperty]
    string email;

    public OnboardingDetailsViewModel(IApiRepository repo, IPopupNavigation popupNavigation)
    {
        repository = repo;
        mopupNavigation = popupNavigation;
    }

    [RelayCommand]
    async Task CreateUser()
    {
        User user = new()
        {
            Name = Name,
            PhoneNumber = PhoneNumber,
            Email = Email,
            AuthId = AuthenticationResult.UniqueId,
        };

        await Shell.Current.GoToAsync(nameof(SecurityPINPage), true, new Dictionary<string, object> 
        {
            {"onboardingUser", user }
        });
    }
}
