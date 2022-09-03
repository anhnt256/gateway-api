using GateWayAPI.IRepository;
using GateWayAPI.Models.GateWay.Order;
using GateWayAPI.Models.GateWay.OrderDetail;
using GateWayAPI.Models.General.ReportModel;
using GateWayManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private IReportRepository _reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }


        [Authorize]
        [HttpPost]
        public IActionResult GetBasicInfo([FromBody] ReportModel report)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var staffId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
            var result = _reportRepository.GetReportInfo(report.StartDate, report.EndDate, staffId);
            if (!string.IsNullOrEmpty(result.OrderJSON))
            {
                result.Orders = JsonConvert.DeserializeObject<List<Orders>>(result.OrderJSON);
                result.OrderJSON = null;
            }
            if (!string.IsNullOrEmpty(result.OrderDetailJSON))
            {
                result.OrderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(result.OrderDetailJSON);
                result.OrderDetailJSON = null;
            }
            return Ok(new { code = ResponseCode.Success, reply = result });
        }
    }
}
