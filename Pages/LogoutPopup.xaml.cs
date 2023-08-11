namespace Moneybase.Pages;

public partial class LogoutPopup 
{
	public LogoutPopup()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Application.Current.Quit();
    }
}