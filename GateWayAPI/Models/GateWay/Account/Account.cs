using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.Account
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int Id { set; get; }
        public string UserName { set; get; }
        public decimal MoneyPaid { set; get; }
        public decimal MoneyUsed { set; get; }
        public decimal RemainMoney { set; get; }
        public string Token { set; get; }
    }
}
