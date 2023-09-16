using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moneybase.Pages.GroupPayPages;
using Moneybase.Services;
using MoneybaseLibrary.Models;
using Mopups.Interfaces;
using MvvmHelpers;

namespace Moneybase.ViewModels;

public partial class GroupPayViewModel : ViewModelBase
{
    [ObservableProperty]
    ObservableRangeCollection<GroupPayParticipant> groupPayParticipants = new();
    //Observable range collection of currencies
    [ObservableProperty]
    ObservableRangeCollection<Currencies> currencies = new();
    [ObservableProperty]
    private Currencies selectedCurrency;
    [ObservableProperty]
    private decimal groupPayTotal;
    [ObservableProperty]
    private decimal initiatorAmount;

    public GroupPayViewModel(IApiRepository apiRepository, IPopupNavigation popupNavigation)
    {
        repository = apiRepository;
        mopupNavigation = popupNavigation;
        loadingPopup = new Pages.LoadingPopup();
        //Get all currencies from enum
        Currencies.AddRange(Enum.GetValues(typeof(Currencies)).Cast<Currencies>());
    }

    [RelayCommand]
    async Task AddParticipant()
    {
        //Group pay participants should not exceed 2
        if (GroupPayParticipants.Count != 2)
        {
            var participant = await Shell.Current.ShowPopupAsync(new AddParticipantPopup(repository));

            if (participant is not null)
            {
                GroupPayParticipants.Add((GroupPayParticipant)participant);
                CalculateGroupPayTotal();
            }
        }
        else
        {
            //Notify user that maximum number of partipants is 3, them included
            await Shell.Current.DisplayAlert("Maximum number of participants reached", "You can only add 2 participants", "OK");
        }
    }

    [RelayCommand]
    async Task PostGroupSessionToApi()
    {
        //check if participants list is empty
        if (GroupPayParticipants.Count == 0)
        {
            await Shell.Current.DisplayAlert("No participants", "You have not added any participants", "OK");
            return;
        }
        else if(InitiatorAmount == 0)
        {
            await Shell.Current.DisplayAlert("No initiator amount", "You have not added an amount for the initiator", "OK");
            return;
        }
        else if (GroupPayTotal == 0)
        {
            await Shell.Current.DisplayAlert("No group pay total", "You have not added a group pay total", "OK");
            return;
        }
        else 
        {
            await mopupNavigation.PushAsync(loadingPopup);
            //Create group pay particiant object for initiator
            GroupPayParticipant initiator = new()
            {
                Participant = await repository.GetUserAsync(UserPhoneNumber),
                ParticipantType = ParticipantType.Initiator,
                Amount = InitiatorAmount
            };

            //Create group pay object
            GroupPay groupPay = new()
            {
                DateTimeCreated = DateTime.Now,
                DateTimeExpiry = DateTime.Now.AddMinutes(30),
                Participants = GroupPayParticipants.ToList(),
                IsActive = true,
                IsPaid = false,
                IsReported = false,
                Currency = SelectedCurrency,
                Amount = GroupPayTotal
            };
            groupPay.Participants.Add(initiator);

            //Post group pay object to API
            bool isSuccessful = await repository.CreateGroupPayment(groupPay);

            if(isSuccessful.Equals(true))
            {
                //Notify user that group pay session has been created
                await Shell.Current.DisplayAlert("Group pay session created", "Your group pay session has been created", "OK");
                await mopupNavigation.PopAsync();
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                //Notify user that group pay session has not been created
                await mopupNavigation.PopAsync();
                await Shell.Current.DisplayAlert("Some error occurred", "Your group pay session has not been created", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

        }
    }
    [RelayCommand]
    async Task ClearParticipantsList()
    {
        //check if participants list is empty
        if (GroupPayParticipants.Count == 0)
        {
            await Toast.Make("Participants list is empty. Click this button to start over making the list").Show();
            return;
        }
        else
        {
            //Ask for confirmation to clear participants list
            bool confirmation = await Shell.Current.DisplayAlert("Clear participants list", "Are you sure you want to clear the participants list?", "Yes", "No");

            //If user confirms, clear participants list
            if (confirmation.Equals(true))
                GroupPayParticipants.Clear();
            else
                return;
        }   
    }

    [RelayCommand]
    async Task RemoveParticant(GroupPayParticipant participant)
    {
        if (participant is null)
            return;

        //Ask for confirmation to remove participant
        var confirmation = Shell.Current.DisplayAlert("Remove participant", "Are you sure you want to remove this participant?", "Yes", "No");

        //If user confirms, remove participant
        if (confirmation.Result.Equals(true))
            GroupPayParticipants.Remove(participant);
        else
            return;
    }

    partial void OnInitiatorAmountChanged(decimal oldValue, decimal newValue)
    {
        CalculateGroupPayTotal();
    }

    private void CalculateGroupPayTotal()
    {
        //Calculate group pay total correct to 2 decimal places
        GroupPayTotal = Math.Round(GroupPayParticipants.Sum(x => x.Amount) + InitiatorAmount, 2);
    }
}
