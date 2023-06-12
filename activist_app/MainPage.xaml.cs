using System.Net.Http.Json;
using System.Text.Json;

namespace activist_app;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
        if (Preferences.Default.ContainsKey("id"))
        {
            Navigation.PushAsync(new Menu());
        }
        InitializeComponent();
    }


    private async void Next_Clicked(object sender, EventArgs e)
    {
        if(Input.Text.Length < 15)
        {
            await DisplayAlert("Ошибка", "Неверный номер профбилета", "ОК");
        } else
        {
            HttpClient client = new HttpClient();
            try {
                var response = await client.GetAsync($"{Methods.APIEndpoint()}/getUser?id={Input.Text}");
                if (response.IsSuccessStatusCode)
                {
                    User u = await JsonSerializer.DeserializeAsync<User>(response.Content.ReadAsStream());

                    Preferences.Default.Set("id", u.id);
                    Preferences.Default.Set("first_name", u.fName);
                    Preferences.Default.Set("last_name", u.lName);
                    Preferences.Default.Set("group", u.group);

                    await Navigation.PushAsync(new Menu());
                }
                else
                {
                    await DisplayAlert("Ошибка", "Профбилет не найден", "ОК");
                }
            } catch 
            {
                await DisplayAlert("Ошибка", "Сервис недоступен", "OK");
            }
        }
    }

    private void idkButton_Clicked(object sender, EventArgs e)
    {
        Browser.Default.OpenAsync(new Uri("https://t.me/profcommospolytech_bot"), BrowserLaunchMode.SystemPreferred);
    }
}

