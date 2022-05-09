using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.CSM.ServiceDetail
{
    public class ServiceDetailContext
    {
        public string ConnectionString { get; set; }

        public ServiceDetailContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public void InsertServiceDetail(ServiceDetail serviceDetail)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string sql = $"Insert Into servicedetailtb (" +
                    $"UserId, " +
                    $"ServiceId, " +
                    $"ServiceDate, " +
                    $"ServiceTime, " +
                    $"ServiceQuantity, " +
                    $"ServiceAmount, " +
                    $"ServicePaid, " +
                    $"Accept, " +
                    $"VoucherId, " +
                    $"StaffId, " +
                    $"PaymentWaitId) " +
                    $"Values ('" +
                    $"{serviceDetail.userId}'," +
                    $"'{serviceDetail.ServiceId}'," +
                    $"'{serviceDetail.ServiceDate}'," +
                    $"'{serviceDetail.ServiceTime}'," +
                    $"'{serviceDetail.ServiceQuantity}'," +
                    $"'{serviceDetail.ServiceAmount}'," +
                    $"'{serviceDetail.ServicePaid}'," +
                    $"'{serviceDetail.Accept}'," +
                    $"'{serviceDetail.VoucherId}'," +
                    $"'{serviceDetail.StaffId}'," +
                    $"'{serviceDetail.PaymentWaitId}'" +
                    $")";

                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

    }
}
