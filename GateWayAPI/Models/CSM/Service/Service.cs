using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GateWayAPI.Models.CSM.Service
{
    public class Service
    {
        public int ServiceId { set; get; }
        public string ServiceName { set; get; }
        public decimal ServicePrice { set; get; }
        public string Unit { set; get; }
        public bool Active { set; get; }
        public int Inventory { set; get; }
        public int WarningInventory { set; get; }
        public int ServiceGroupId { set; get; }
        public int NumInventoryExpected { set; get; }
        public int UnitId { set; get; }
        public int SuggestId { set; get; }
        public Service() { }

        public Service(MySqlDataReader dr)
        {
            ServiceId = int.Parse(dr["ServiceId"].ToString());
            ServiceName = dr["ServiceName"].ToString();
            ServicePrice = decimal.Parse(dr["ServicePrice"].ToString());
            Unit = dr["Unit"].ToString();
            Inventory = int.Parse(dr["Inventory"].ToString());
            WarningInventory = int.Parse(dr["WarningInventory"].ToString());
            ServiceGroupId = int.Parse(dr["ServiceGroupId"].ToString());
            NumInventoryExpected = int.Parse(dr["NumInventoryExpected"].ToString());
            UnitId = int.Parse(dr["UnitId"].ToString());
            SuggestId = int.Parse(dr["SuggestId"].ToString());
        }
    }
}
