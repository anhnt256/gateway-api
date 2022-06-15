using GateWayAPI.IRepository;
using GateWayManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var result = _productRepo.GetAllProduct();
            return Ok(new { code = ResponseCode.Success, reply = result });
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetProductByType(int ProductTypeId)
        {
            var result = _productRepo.GetProductByType(ProductTypeId);
            return Ok(new { code = ResponseCode.Success, reply = result });
        }
    }
}
