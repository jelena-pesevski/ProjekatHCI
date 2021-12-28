using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProjekatHCI.Util
{
    public class ConnectionPool
    {
        private static ConnectionPool instance=null;
        private static List<MySqlConnection> freeConnections;
        private static List<MySqlConnection> usedConnections;

        public static ConnectionPool GetInstance()
        {
            if (instance == null)
            {
                instance = new ConnectionPool();
            }
            return instance;
        }
        private ConnectionPool()
        {
            freeConnections = new List<MySqlConnection>();
            usedConnections = new List<MySqlConnection>();
            for(int i=0; i<Constants.PRECONNECT_COUNT; i++)
            {
                freeConnections.Add(new MySqlConnection(Constants.CONN_STRING));
            }
        }

        public MySqlConnection CheckOut()
        {
            MySqlConnection conn = null;
            if (freeConnections.Count > 0)
            {
                conn = freeConnections[0];
                conn.Open();
                freeConnections.RemoveAt(0);
                usedConnections.Add(conn);
            }
            else
            {
                conn = new MySqlConnection(Constants.CONN_STRING);
                conn.Open();
                usedConnections.Add(conn);
            }
            return conn;
        }


        public void CheckIn(MySqlConnection conn)
        {
            if (conn == null)
            {
                return;
            }
            if (usedConnections.Remove(conn))
            {
                conn.Close();
                freeConnections.Add(new MySqlConnection(Constants.CONN_STRING));
                while(freeConnections.Count> Constants.MAX_IDLE_CONNECTIONS)
                {
                    int lastOne = freeConnections.Count - 1;
                    MySqlConnection c = freeConnections[lastOne];
                    freeConnections.RemoveAt(lastOne);
                    c.Close();
                }
            }
        }

    }
}
