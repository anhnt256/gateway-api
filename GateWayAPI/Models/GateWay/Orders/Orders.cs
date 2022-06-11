using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.Order
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int Id { set; get; }
        public int? VoucherId { set; get; }
        public int AccountId { set; get; }
        public int TotalItem { set; get; }
        public decimal TotalMoney { set; get; }
        public int Status { set; get; }
        public string Note { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public int? UpdatedBy { set; get; }

    }
}
