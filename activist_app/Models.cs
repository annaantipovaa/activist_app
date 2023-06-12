using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activist_app
{
    public class User
    {
        public string id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string group { get; set; }
    }

    public class RatingRow
    {
        public User user { get; set; }
        public int points { get; set; }
    }

    public class Event
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string registrationLink { get; set; }
        public string img { get; set; }
        public string timeStart { get; set; }
    }
}
