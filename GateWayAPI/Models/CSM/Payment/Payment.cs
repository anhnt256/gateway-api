using Dapper.Contrib.Extensions;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.CSM.Payment
{
    [Table("paymenttb")]
    public class Payment
    {
        public int VoucherId { set; get; }
        public int UserId { set; get; }
        public int Amount { set; get; }
        public int PaymentType { set; get; }
        //public DateTime ServeDate { set; get; }
        //public DateTime ServeTime { set; get; }
    }
}
