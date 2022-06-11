using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.OrderDetail
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int Id { set; get; }
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal SalePrice { set; get; }
    }
}
