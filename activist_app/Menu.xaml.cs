namespace activist_app;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();

       
    }

    private void Profile_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Profile());
    }

    private void Events_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Events());
    }

    
}