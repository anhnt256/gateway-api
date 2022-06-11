using GateWayAPI.IRepository;
using GateWayAPI.Models.GateWay.Order;
using GateWayAPI.Models.General.OrderModel;
using GateWayManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GateWayAPI.Controllers
{
    [Route("order")]
    public class OrderController : Controller
    {
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        private GameController _gameController;
        public OrderController(IProductRepository productRepository, GameController gameController, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _gameController = gameController;
            _orderRepository = orderRepository;
        }
        [HttpGet]
        [Route("product-type")]
        public IActionResult GetProductType()
        {
            var response = _productRepository.GetAllProductType();
            return Ok(new { code = ResponseCode.Success, reply = response });
        }

        [HttpGet]
        [Route("product")]
        public IActionResult GetProduct()
        {
            var response = _productRepository.GetAllProduct();
            return Ok(new { code = ResponseCode.Success, reply = response });
        }

        [HttpGet]
        [Route("option")]
        public IActionResult GetOptionByProductId(int productId) {
            var response = _productRepository.GetProductOptionByProductId(productId);
            return Ok(new { code = ResponseCode.Success, reply = response });
        }

        [Authorize]
        [HttpPost]
        [Route("submit")]
        public ActionResult SubmitCart([FromBody] OrderModel orderModel)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            int accountId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
            if (accountId == 0)
            {
                return Ok(new { code = ResponseCode.RequiredLogin, reply = "" });
            }
            Orders order = new Orders();
            order.TotalItem = orderModel.TotalItem;
            order.TotalMoney = orderModel.TotalMoney;
            order.CreatedDate = DateTime.Now;
            order.Status = (int)OrderStatus.Received;
            order.AccountId = accountId;
            order.Note = "";

            var orderId = _orderRepository.InsertOrder(order);

            if (orderId != 0) {
                foreach (var detail in orderModel.Details)
                {
                    detail.OrderId = int.Parse(orderId.ToString());
                    _orderRepository.InsertOrderDetail(detail);
                }
            }

            return Ok();
        }
    }
}
