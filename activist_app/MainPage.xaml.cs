namespace activist_app;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}


    private void Next_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Menu());
    }
}

