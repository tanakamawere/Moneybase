using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages.Startup;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Moneybase.ViewModels;

public partial class SignUpViewModel : ViewModelBase
{
    [ObservableProperty]
    string name;
    [ObservableProperty]
    string phoneNumber;
    [ObservableProperty]
    string email;
    [ObservableProperty]
    string oTP;

    public SignUpViewModel(IApiRepository repo, IPopupNavigation popupNavigation)
    {
        repository = repo;
        mopupNavigation = popupNavigation;
        loadingPopup = new Pages.LoadingPopup();
    }

    [RelayCommand]
    async Task CreateUser()
    {
        string response = await Shell.Current.DisplayActionSheet($"Is the following number +263-{PhoneNumber} correct?", null, null,"Yes", "No");

        if (response == "Yes")
        {
            try
            {
                await mopupNavigation.PushAsync(loadingPopup);
                FirstTimeUser userIsNew = await repository.UserIsNew(PhoneNumber);
                if (userIsNew.IsNew.Equals(true))
                {
                    await mopupNavigation.PopAsync();
                    await ProceedWithCreation();
                }
                else
                {
                    await mopupNavigation.PopAsync();
                    await Shell.Current.DisplayAlert("Account already created", "This number is already associated with an account. Login instead.", "Okay");
                }

            }
            catch (Exception)
            {
                await mopupNavigation.PopAsync();
                await Shell.Current.DisplayAlert("A bit of a niggle there", "These things happen from time to time. Please try again the operation", "Okay");
            }
        }
    }

    private async Task ProceedWithCreation()
    {
        try
        {
            await mopupNavigation.PushAsync(loadingPopup);
            User user = new()
            {
                Name = Name,
                PhoneNumber = PhoneNumber,
            };

            SendOTP();

            await Shell.Current.GoToAsync(nameof(OTPPage), true, new Dictionary<string, object>
                {
                    {"userToCreate", user},
                    {"otpGenerated", OTP },
                });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Something bad has just happened: {ex.Message}", "I understand");
        }
        finally
        {
            await mopupNavigation.PopAsync();
        }
    }

    private void SendOTP()
    {
        string accountSid = "ACc9c71b26684e5778c75865a3a54644ed";
        string authToken = "2613bd06c386437a69273c40b2b97429";

        OTP = GenerateOTP();
        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(new PhoneNumber($"+263{PhoneNumber}"));
        messageOptions.From = new PhoneNumber("+17079860347");
        messageOptions.Body = $"Your Moneybase OTP is {OTP}";
        var message = MessageResource.Create(messageOptions);
    }

    private string GenerateOTP()
    {
        Random random = new Random();
        int otpNumber = random.Next(100000, 999999);
        return otpNumber.ToString();
    }
}
