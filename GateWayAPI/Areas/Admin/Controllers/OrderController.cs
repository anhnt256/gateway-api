using GateWayAPI.IRepository;
using GateWayAPI.Models.GateWay.OrderDetail;
using GateWayAPI.Models.GateWay.ProductOption;
using GateWayManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var orders = _orderRepository.GetAllOrder();
            if (orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    order.Details = JsonConvert.DeserializeObject<List<OrderDetail>>(order.OrderDetail);
                    if (order.Details.Count > 0)
                    {
                        foreach (var detail in order.Details)
                        {
                            if (!string.IsNullOrEmpty(detail.JsonOptions))
                            {
                                detail.Options = JsonConvert.DeserializeObject<List<ProductOption>>(detail.JsonOptions);
                            }
                        }
                    }
                }
            }
            return Ok(new { code = ResponseCode.Success, reply = orders });
        }
    }
}
