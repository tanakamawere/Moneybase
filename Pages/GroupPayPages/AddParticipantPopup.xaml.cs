using CommunityToolkit.Maui.Views;
using Moneybase.Services;
using MoneybaseLibrary.Models;

namespace Moneybase.Pages.GroupPayPages;

public partial class AddParticipantPopup : Popup
{
	private GroupPayParticipant participant = new();
	private readonly IApiRepository repository;
	public bool IsBusy = false;

	//Set code behind as binding context
	public AddParticipantPopup(IApiRepository repo)
	{
        InitializeComponent();
        repository = repo;
    }

	//This method is called when the popup is closed
	void AddParticipant(object sender, EventArgs e)
	{
        //check if amountToPayEntry is not null
		if (amountToPayEntry.Text != null)
        {
			participant.Amount = decimal.Parse(amountToPayEntry.Text);
            if (participant is not null)
            {
                participant.ParticipantType = ParticipantType.Participant;

                CloseAsync(participant);
            }
            else
                return;
        }
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
	{ 
		IsBusy = true;

		loadingIndicator.IsRunning = true;
        loadingIndicator.IsVisible = true;
        participantName.IsVisible = false;

        //Check if length of entry is equal to 9
        if (e.NewTextValue.Length > 8)
		{
			repository.GetUserAsync(e.NewTextValue).ContinueWith((user) =>
			{
                if (user.Result != null)
				{
					participant.ParticipantPhoneNumber = user.Result.PhoneNumber;
					participantName.Text = user.Result.Name;
                }
            });
        }
        loadingIndicator.IsRunning = false;
        loadingIndicator.IsVisible = false;
        participantName.IsVisible = true;
    }
}