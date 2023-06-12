namespace activist_app;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
        firstName.Text = Preferences.Get("first_name", "error");
        lastName.Text = Preferences.Get("last_name", "error");
        group.Text = Preferences.Get("group", "error");
	}

    private void Rating_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Rating());
    }
}