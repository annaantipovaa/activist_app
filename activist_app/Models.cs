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
}
