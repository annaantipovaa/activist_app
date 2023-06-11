using Microsoft.Data.Sqlite;

namespace activist_api
{
    public class Response
    {
        
    }

    public class User
    {
        public string Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string Group { get; set; }

        public User(string id, string fname, string lname, string group)
        {
            Id = id;
            fName = fname;
            lName = lname;
            Group = group;
        }
    }

    public class UserRating
    {
        public int id { get; set; }
        public int points { get; set; }

        public UserRating(int id, int points)
        {
            this.id = id;
            this.points = points;
        }
    }

    public class Event
    {
        public int id;
        public string name;
        public string description;
        public string registrationLink;
        public string img;
        public string timeStart;
        public string timeEnd;

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
            this.id = reader.GetInt32(0);
            this.name = reader.GetString(1);
            this.description = reader.GetString(2);
            this.registrationLink = reader.GetString(3);
            this.img = reader.GetString(4);
            this.description = reader.GetString(5);
            this.img = reader.GetString(6);
            this.timeStart = reader.GetString(6);
            this.timeEnd = reader.GetString(7);
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
