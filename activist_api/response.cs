using Microsoft.Data.Sqlite;

namespace activist_api
{
    public class Response
    {
        
    }

    public class User
    {
        private bool exists = false;
        public string? Id { get; set; }
        public string? fName { get; set; }
        public string? lName { get; set; }
        public string? Group { get; set; }

        bool isExist()
        {
            return exists;
        }

        public User(string id, string fname, string lname, string group)
        {
            Id = id;
            fName = fname;
            lName = lname;
            Group = group;
        }

        public User(SqliteDataReader reader) {
            this.Id = reader.GetString(0);
            this.fName = reader.GetString(1);
            this.lName = reader.GetString(2);
            this.Group = reader.GetString(3);
        }

        public User(string id)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                var command = new SqliteCommand($"SELECT * FROM users WHERE id={id}", connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        this.exists = true;
                        reader.Read();

                        this.Id = reader.GetString(0);
                        this.fName = reader.GetString(1);
                        this.lName = reader.GetString(2);
                        this.Group = reader.GetString(3);
                    } else
                    {
                        this.exists = false;
                    }
                }
            }

        }
    }

    public class UserRating
    {
        //public string name { get; set; }
        public User user { get; set; }
        public int points { get; set; }

        public UserRating(SqliteDataReader reader)
        {
            this.user = new User(reader.GetString(0));
            this.points = reader.GetInt32(1);
        }
    }

    public class Event
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string registrationLink { get; set; }
        public string img { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }

        public Event(int id, string name, string description, string registrationLink, string img, string timeStart, string timeEnd)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.registrationLink = registrationLink;
            this.img = img;
            this.timeStart = timeStart;
            this.timeEnd = timeEnd;
        }

        public Event(SqliteDataReader reader)
        {
            id = reader.GetInt32(0);
            name = reader.GetString(1);
            description = reader.GetString(2);
            registrationLink = reader.GetString(3);
            img = reader.GetString(4);
            timeStart = reader.GetString(5);
            timeEnd = reader.GetString(6);
        }

        DateTime getStart()
        {
            return DateTime.ParseExact(this.timeStart, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);
        }

        DateTime getEnd()
        {
            return DateTime.ParseExact(this.timeEnd, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
