﻿using CommunityToolkit.Mvvm.Input;
using Moneybase.Services;
using Mopups.Interfaces;

namespace Moneybase.ViewModels;

public partial class SecurityPINViewModel : ViewModelBase
{
    public SecurityPINViewModel(IApiRepository repo, IPopupNavigation popup)
    {
        repository = repo;
        mopupNavigation = popup;
        loadingPopup = new Pages.LoadingPopup();
    }


    [RelayCommand]
    public async Task CheckPin(string code)
    {

        //if response is true, it means the user's pin is correct. Move on
        await mopupNavigation.PushAsync(loadingPopup);
        try
        {
            bool response = await repository.CheckPIN(UserPhoneNumber, code);
            if (response.Equals(true))
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await Shell.Current.DisplayAlert("Pin is incorrect", "Re-enter your pin", "Cancel");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Account not found", ex.Message, "Ok");
        }
        await mopupNavigation.PopAsync();
    }
}
