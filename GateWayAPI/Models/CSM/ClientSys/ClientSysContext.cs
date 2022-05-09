using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.CSM.ClientSys
{
    public class ClientSysContext
    {
        public string ConnectionString { get; set; }

        public ClientSysContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public int GetUserIdFromIpAddress(string hostName)
        {
            int userId = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = $"Select * from clientsystb where IP='{hostName}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userId = int.Parse(reader["UserId"].ToString());
                    }
                    conn.Close();
                }
            }
            return userId;
        }
    }
}
