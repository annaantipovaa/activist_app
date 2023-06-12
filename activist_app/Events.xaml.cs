using Microsoft.Maui.Controls;
using System.Text.Json;
using System.Windows.Input;

namespace activist_app;

public partial class Events : ContentPage
{
    public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    public Events()
	{
		InitializeComponent();
	}

    private void Event_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Event1());
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        try
        {
            List<Event> events;
            var response = await client.GetAsync($"{Methods.APIEndpoint()}/getEvents");
            if (response.IsSuccessStatusCode)
            {
                events = await JsonSerializer.DeserializeAsync<List<Event>>(response.Content.ReadAsStream());
                foreach (var item in events)
                {
                    Grid eventRow = new Grid
                    {
                        
                        RowDefinitions =
                        {
                            new RowDefinition{Height = new GridLength(10, GridUnitType.Auto)},
                            new RowDefinition{Height = new GridLength(10, GridUnitType.Auto)},
                            new RowDefinition{Height = new GridLength(10, GridUnitType.Auto)},
                            new RowDefinition{Height = new GridLength(10, GridUnitType.Auto)},
                            new RowDefinition{Height = new GridLength(10, GridUnitType.Auto)}
                        }
                    };

                    eventRow.Add(new Image
                    {
                        Source = new Uri(item.img),
                        HeightRequest = 400,
                        WidthRequest = 200,
                        Margin = new Thickness(30)
                        
                    }, 0, 0); ;

                    eventRow.Add(new Label
                    {
                        Text = item.name, FontFamily = "Gilroy", 
                        FontAttributes = FontAttributes.Bold, 
                        FontSize = 30,
                        HorizontalOptions = LayoutOptions.Center, 
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(20)
                        
                    }, 0, 1);

                    eventRow.Add(new Label
                    {
                        Text = item.description,
                        FontFamily = "Gilroy",
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(20)
                        
                    }, 0, 2);

                    DateTime date = DateTime.ParseExact(item.timeStart, "yyyyMMddTHH:mm:ssZ", new System.Globalization.CultureInfo("ru-RU"));

                    eventRow.Add(new Label
                    {
                        Text = date.ToString("dddd, d MMMM, H:mm", new System.Globalization.CultureInfo("ru-RU")),
                        FontFamily = "Gilroy",
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 14,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(20)
                        
                    }, 0, 3); ;

                    eventRow.Add(new Button
                    {
                        Text = "Зарегистрироваться",
                        FontFamily = "Gilroy",
                        HeightRequest = 80,
                        WidthRequest = 230,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 18,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(0, 20, 0, 0),
                        CornerRadius = 35,
                        Command = TapCommand,
                        CommandParameter = item.registrationLink,
                        BackgroundColor = Color.FromRgb(X, Y, 0),
                        
                    }, 0, 4);

                    eventsLayout.Children.Add(eventRow);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"{ex.Message}\nСервис недоступен", "OK");
        }
    }
}