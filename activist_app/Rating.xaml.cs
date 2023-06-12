using System.Text.Json;

namespace activist_app;

public partial class Rating : ContentPage
{
	public Rating()
	{
		InitializeComponent();

		
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        

        HttpClient client = new HttpClient();
        try
        {
            List<RatingRow> rating;
            var response = await client.GetAsync($"{Methods.APIEndpoint()}/getRating");
            if (response.IsSuccessStatusCode)
            {
                rating = await JsonSerializer.DeserializeAsync<List<RatingRow>>(response.Content.ReadAsStream());
                foreach (var item in rating)
                {
                    Grid ratingRow = new Grid
                    {
                        ColumnDefinitions =
                    {
                        new ColumnDefinition(),
                        new ColumnDefinition()
                    }
                    };

                    ratingRow.Add(new Label
                    {
                        Text = $"{item.user.fName} {item.user.lName}\n{item.user.group}"
                    }, 0, 0);

                    ratingRow.Add(new Label
                    {
                        Text = item.points.ToString()
                    }, 1, 0);

                    if(Preferences.Get("id", "error") == item.user.id)
                    {
                        ratingRow.Background = new SolidColorBrush(Colors.Yellow);
                    }

                    ratingLayout.Children.Add(ratingRow);
                }
            }
        }
        catch
        {
            await DisplayAlert("Ошибка", "Сервис недоступен", "OK");
        }
    }
}