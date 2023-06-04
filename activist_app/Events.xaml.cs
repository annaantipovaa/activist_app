namespace activist_app;

public partial class Events : ContentPage
{
	public Events()
	{
		InitializeComponent();
	}

    private void Event_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Event1());
    }
}