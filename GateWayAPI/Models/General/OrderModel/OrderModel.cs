using GateWayAPI.Models.GateWay.Order;
using GateWayAPI.Models.GateWay.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.General.OrderModel
{
    public class OrderModel: Orders
    {
        public List<OrderDetail> Details { set; get; }
        public string OrderDetail { set; get; }
    }
}
