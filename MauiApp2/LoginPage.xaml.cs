namespace MauiApp2;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void forgotButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ForgotPage());
    }

    private async void LoginButtonClicked(object sender, EventArgs e)
    {
        try
        {
                if (string.IsNullOrWhiteSpace(emailEntry.Text)
               || string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                warningLabelLogin.Text = "Please complete the form";
                return;
            }
            //checkEmailExists- method returns null if there is no person with the given E-mail
            if (await App.Database.checkEmailExists(emailEntry.Text) == null)
            {
                warningLabelLogin.Text = "There is no person with the given E-mail";
                return;
            }
            //returns null if E-mail and password do not match
            if (await App.Database.check_EmailPasswordPair_Exists(emailEntry.Text, passwordEntry.Text) == null)
            {
                warningLabelLogin.Text = "The given E-mail and the password do not match";
                return;
            }
        }
        catch(SQLite.SQLiteException SQLex)
        {
            await DisplayAlert("SQL exception", SQLex.Message, "OK");
        }
         await Navigation.PushAsync(new NaviPage());
         emailEntry.Text = passwordEntry.Text = warningLabelLogin.Text = String.Empty;
    }

    private async void registerButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    private async void weatherPageButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WeatherPage());
    }
    private async void navigationPageButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NaviPage());
    }
}

