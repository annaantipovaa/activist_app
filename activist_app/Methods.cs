using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activist_app
{
    internal class Methods
    {
        public static string APIEndpoint()
        {
            return Preferences.Default.Get("api_endpoint", "http://sadsadasdasdasdasdas");
        }

        public static string UserId()
        {
            return Preferences.Default.Get("id", "null");
        }
    }

    
}
