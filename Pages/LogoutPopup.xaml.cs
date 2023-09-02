using Mopups.Services;

namespace Moneybase.Pages;

public partial class LogoutPopup 
{
	public LogoutPopup()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		MopupService.Instance.PopAsync();
    }
}