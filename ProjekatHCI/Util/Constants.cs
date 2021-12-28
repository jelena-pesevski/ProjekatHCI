using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ProjekatHCI.Util
{
    class Constants
    {
        public static readonly string CONN_STRING = ConfigurationManager.ConnectionStrings["MySqlDatabase"].ConnectionString;
        public static readonly int PRECONNECT_COUNT = 10;
        public static readonly int MAX_IDLE_CONNECTIONS = 10;
    }
}
