using activist_api;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (!File.Exists(@"data.db"))
{
    using (var connection = new SqliteConnection("Data Source=data.db"))
    {
        connection.Open();
        SqliteCommand command = new SqliteCommand("CREATE TABLE \"events\" ( \"name\" TEXT, \"id\" INTEGER, \"description\" TEXT, \"registrationLink\" TEXT, \"img\" TEXT, \"timeStart\" TEXT, \"timeEnd\" TEXT, \"registered\" TEXT );" +
            "CREATE TABLE \"rating\" ( \"id\" INTEGER, \"points\" INTEGER );" +
            "CREATE TABLE \"users\" ( \"id\" INTEGER, \"fName\" INTEGER, \"lName\" TEXT, \"group\" TEXT );", connection);

        command.ExecuteNonQuery();
        connection.Close();
    }
}

app.MapGet("/status", () => Results.Ok());

app.MapGet("/getUser", async (context) =>
{
    using (var connection = new SqliteConnection("Data Source=data.db"))
    {
        connection.Open();

        if (context.Request.Query["id"] == "")
        {
            await Results.BadRequest().ExecuteAsync(context);
        }
        SqliteCommand command = new SqliteCommand($"SELECT * FROM users WHERE id={context.Request.Query["id"]}", connection);
        using (SqliteDataReader dr = command.ExecuteReader())
        {
            if (dr.HasRows)
            {
                dr.Read();
                User u = new User(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3));
                await Results.Json(u).ExecuteAsync(context);
            } else
            {
                await Results.NotFound().ExecuteAsync(context);
            }
        }
        connection.Close();
    }
});

app.MapGet("/getRating", async (context) =>
{
    using (var connection = new SqliteConnection("Data Source=data.db"))
    {
        connection.Open();
        SqliteCommand command = new SqliteCommand("SELECT * FROM rating ORDER BY points DESC", connection);
        using (SqliteDataReader dr = command.ExecuteReader())
        {
            if (dr.HasRows)
            {
                List<UserRating> r = new List<UserRating>();
                while (dr.Read())
                {
                    r.Add(new UserRating(dr.GetInt32(0), dr.GetInt32(1)));
                }

                await Results.Json(r).ExecuteAsync(context);
            }
            else
            {
                await Results.BadRequest().ExecuteAsync(context);
            }
        }
        connection.Close();
    }
});

app.MapGet("/getEvents", async (context) =>
{
    using (var connection = new SqliteConnection("Data Source=data.db"))
    {
        connection.Open();
        SqliteCommand command = new SqliteCommand("SELECT * FROM events", connection);
        using(SqliteDataReader dr = command.ExecuteReader())
        {
            if (dr.HasRows)
            {
                List<Event> events = new List<Event>();
                while (dr.Read())
                {
                    events.Add(new Event(dr));
                }

                await Results.Json(events).ExecuteAsync(context);
            }
        }
        connection.Close();
    }
});

app.Run();
