using System.Net;
using System.Net.Mail;

namespace MauiApp2;

public partial class ForgotPage : ContentPage
{
    public ForgotPage()
    {
        InitializeComponent();
    }

    private async void resetPasswordButtonClicked(object sender, EventArgs e)
    {
        try
        {
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.outlook.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential("RaiseAgriculture@hotmail.com", "RSAGRCLTR1");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("RaiseAgriculture@hotmail.com", "Raise Agriculture");
            mail.To.Add(forgotPageEmailEntry.Text);
            mail.Subject = "Reset Password";
            mail.Body = "Hello,\nYou sent us a request to reset your password. By clicking the following link you " +
                "can reset your password:\nhttps://raiseagri.com \nKind regards, Raise Agriculture"; 
            sc.Send(mail);

            forgotPageEmailEntry.Text = String.Empty;
            await Navigation.PushAsync(new PasswordResetMailSentPage());

        }
        catch (Exception ex)
        {
            await DisplayAlert("Failed to send", ex.Message, "OK");
        }
    }
}
