namespace MauiApp2;

public partial class PasswordResetMailSentPage : ContentPage
{
	public PasswordResetMailSentPage()
	{
		InitializeComponent();
	}

    private async void toLoginPageButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}