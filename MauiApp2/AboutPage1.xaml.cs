namespace MauiApp2;

public partial class AboutPage1 : ContentPage
{
	public AboutPage1()
	{
		InitializeComponent();
	}

    private async void AboutButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WebPage());
    }
}