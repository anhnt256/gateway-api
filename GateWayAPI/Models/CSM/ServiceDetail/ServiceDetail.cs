using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GateWayAPI.Models.CSM.ServiceDetail
{
    public class ServiceDetail
    {
        public int ServiceDetailId { set; get; }
        public int userId { set; get; }
        public int ServiceId { set; get; }
        public string ServiceDate { set; get; }
        public string ServiceTime { set; get; }
        public int ServiceQuantity { set; get; }
        public decimal ServiceAmount { set; get; }
        public int ServicePaid { set; get; }
        public int Accept { set; get; }
        public int VoucherId { set; get; }
        public int StaffId { set; get; }
        public int PaymentWaitId { set; get; }
        public ServiceDetail() { }

        public ServiceDetail(MySqlDataReader dr)
        {
            ServiceDetailId = int.Parse(dr["ServiceDetailId"].ToString());
            userId = int.Parse(dr["UserId"].ToString());
            ServiceId = int.Parse(dr["ServiceId"].ToString());
            ServiceDate = dr["ServiceDate"].ToString();
            ServiceTime = dr["ServiceTime"].ToString();
            ServiceQuantity = int.Parse(dr["ServiceQuantity"].ToString());
            ServiceAmount = decimal.Parse(dr["ServiceAmount"].ToString());
            ServicePaid = int.Parse(dr["ServicePaid"].ToString());
            Accept = int.Parse(dr["Accept"].ToString());
            VoucherId = int.Parse(dr["VoucherId"].ToString());
            StaffId = int.Parse(dr["StaffId"].ToString());
            PaymentWaitId = int.Parse(dr["PaymentWaitId"].ToString());
        }
    }
}
