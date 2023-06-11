using System.Net.Http.Json;

namespace activist_app;

public partial class MainPage : ContentPage
{
    public class User
    {
        public string id;
        public string fName;
        public string lName;
        public string group;
    }

	public MainPage()
	{
		InitializeComponent();
        if(Preferences.Default.ContainsKey("id")) {
            Navigation.PushAsync(new Menu());
        }
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
                    User u = await response.Content.ReadFromJsonAsync<User>();

                    Preferences.Default.Set("id", u.id);
                    Preferences.Default.Set("fName", u.fName);
                    Preferences.Default.Set("lName", u.lName);
                    Preferences.Default.Set("lName", u.group);

                    await Navigation.PushAsync(new Menu());
                }
                else
                {
                    await DisplayAlert("Ошибка", "Профбилет не найден", "ОК");
                }
            } catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"{Methods.APIEndpoint()}\n{ex.Message}\nСервис недоступен", "OK");
            }
        }

    }

    private void idkButton_Clicked(object sender, EventArgs e)
    {
        Browser.Default.OpenAsync(new Uri("https://t.me/profcommospolytech_bot"), BrowserLaunchMode.SystemPreferred);
    }
}

