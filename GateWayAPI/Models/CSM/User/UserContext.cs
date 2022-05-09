using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.CSM.User
{
    public class UserContext
    {
        public string ConnectionString { get; set; }

        public UserContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<User> GetAllUsers()
        {
            List<User> list = new List<User>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from usertb", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new User()
                        {
                            ID = reader["ID"].ToString(),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            UserName = reader["UserName"].ToString(),
                            Active = Convert.ToInt32(reader["Active"]),
                        });
                    }
                    conn.Close();
                }
            }
            return list;
        }


        public void UpdateUserById(int UserId, int moneyPaid, int remainMoney)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string sql = $"Update usertb SET MoneyPaid='{moneyPaid}', RemainMoney='{remainMoney}' Where UserId='{UserId}'";

                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public User GetUserFromId(int UserId)
        {
            User u = new User();
            using (MySqlConnection conn = GetConnection())
            {
                string sql = $"select * from usertb Where UserId='{UserId}'";

                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        u.ID = reader["ID"].ToString();
                        u.UserId = Convert.ToInt32(reader["UserId"]);
                        u.UserName = reader["UserName"].ToString();
                        u.Active = Convert.ToInt32(reader["Active"]);
                        u.MoneyPaid = Convert.ToInt32(reader["MoneyPaid"]);
                        u.MoneyUsed = Convert.ToInt32(reader["MoneyUsed"]);
                        u.RemainTime = Convert.ToInt32(reader["RemainTime"]);
                        u.RemainMoney = Convert.ToInt32(reader["RemainMoney"]);
                        u.FreeTime = Convert.ToInt32(reader["FreeTime"]);
                        u.FreeMoney = Convert.ToInt32(reader["FreeMoney"]);
                    }
                    conn.Close();
                }
            }
            return u;
        }
    }
}
