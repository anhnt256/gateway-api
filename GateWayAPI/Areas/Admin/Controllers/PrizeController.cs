using GateWayAPI.IRepository;
using GateWayAPI.IRepository.CSM;
using GateWayAPI.IRepository.GateWay;
using GateWayAPI.Models.GateWay.GameResultDetail;
using GateWayAPI.Models.General.GameResultModel;
using GateWayManagement.Models;
using GateWayManagement.Models.GateWay.GameResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PrizeController : Controller
    {
        private IGameRepository _gameRepo;
        private IServiceRepository _serviceRepo;

        public PrizeController(IGameRepository gameRepo, IServiceRepository serviceRepo)
        {
            _gameRepo = gameRepo;
            _serviceRepo = serviceRepo;
        }

        [Authorize]
        public IActionResult Index()
        {
            var result = _serviceRepo.GetAllService();
            return Ok(new { code = ResponseCode.Success, reply = result });
        }

        [Authorize]
        [HttpGet]
        public IActionResult ExchangePrize(string Code, string machineName)
        {
            var result = _gameRepo.GetResultByCode(Code);
            if (result != null)
            {
                int isExpired = DateTime.Compare(result.ExpiratedDate.Value, DateTime.Now);
                if (isExpired <= 0)
                {
                    return Ok(new { code = ResponseCode.ParamsInvalid, reply = "Mã đổi thưởng đã hết hạn sử dụng" });
                }
                return Ok(new { code = ResponseCode.Success, reply = result });
            }
            return Ok(new { code = ResponseCode.ParamsInvalid, reply = "Mã đổi thưởng không đúng hoặc đã được sử dụng" });
        }

        [Authorize]
        [HttpPost]
        public IActionResult UseCode([FromBody] GameResultModel model)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            model.ExchangeBy = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
            model.ExchangeDate = DateTime.Now;
            model.IsUsed = true;
            var result = _gameRepo.UseCode(model);
            if (model.Detail != null && model.Detail.Count > 0)
            {
                foreach (var detail in model.Detail)
                {
                    detail.GameResultId = model.GameResultId;
                    _gameRepo.InsertGameResultDetail(detail);
                }
            }

            return Ok(new { code = ResponseCode.Success, reply = result });
        }

    }
}
