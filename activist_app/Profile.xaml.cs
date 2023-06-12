namespace activist_app;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
        firstName.Text = Preferences.Get("first_name", "error");
        lastName.Text = Preferences.Get("last_name", "error");
        group.Text = Preferences.Get("group", "error");

        firstName.FontFamily = "Gilroy";
        lastName.FontFamily = "Gilroy";
        group.FontFamily = "Gilroy";

        firstName.FontAttributes = FontAttributes.Bold;
        lastName.FontAttributes = FontAttributes.Bold;
        group.FontAttributes = FontAttributes.Bold;
    }

    private void Rating_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Rating());
    }
}