using MauiApp2.UserDatabase;

namespace MauiApp2;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void registerButtonRegPage_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(emailAgainEntry.Text)
            || string.IsNullOrWhiteSpace(passwordEntry.Text) || string.IsNullOrWhiteSpace(passwordAgainEntry.Text)
            || string.IsNullOrWhiteSpace(nameEntry.Text) || string.IsNullOrWhiteSpace(surnameEntry.Text))
        {
            warningLabel.Text = "Please complete the form";
            return;
        }

        if (!emailEntry.Text.Equals(emailAgainEntry.Text))
        {
            warningLabel.Text = "E-mails do not match";
            return;
        }
        if (!passwordEntry.Text.Equals(passwordAgainEntry.Text))
        {
            warningLabel.Text = "Passwords do not match";
            return;
        }
        //checkEmailExists- method returns null if there is no person with the given email
        if (await App.Database.checkEmailExists(emailEntry.Text) != null)
        {
            warningLabel.Text = "A user with this E-mail already exists";
            return;
        }

        await App.Database.addPersonAsync(new Person(nameEntry.Text,
            surnameEntry.Text, emailEntry.Text, passwordEntry.Text));

        nameEntry.Text = surnameEntry.Text = emailEntry.Text = passwordEntry.Text
            = emailAgainEntry.Text = passwordAgainEntry.Text = warningLabel.Text = String.Empty;
    }

    private async void loginButtonRegPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}
