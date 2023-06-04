namespace activist_app;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
	}

    private void Rating_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Rating());
    }
}