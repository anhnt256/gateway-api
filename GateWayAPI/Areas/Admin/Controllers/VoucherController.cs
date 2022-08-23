using GateWayAPI.IRepository;
using GateWayAPI.IRepository.GateWay;
using GateWayAPI.Models.GateWay.General.GameModel;
using GateWayManagement.Models;
using GateWayManagement.Models.GateWay.GameResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VoucherController : Controller
    {
        private IAccountRepository _accountRepository;
        private IGameRepository _gameRepository;
        public VoucherController(IGameRepository gameRepository, IAccountRepository accountRepository)
        {
            _gameRepository = gameRepository;
            _accountRepository = accountRepository;
        }
        string EndGame = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Config")["EndVoucher"];
        string StartComboSang = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Config")["StartComboSang"];
        string StartComboToi = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Config")["StartComboToi"];

        public IActionResult ExchangeVoucher([FromBody] GameModel gameModel)
        {

            if (!gameModel.userName.ToLower().StartsWith("combo"))
            {
                return Ok(new { code = ResponseCode.ServerError, reply = "Tài khoản không hợp lệ" });
            }
            else
            {
                if (gameModel.gameTypeId == 3)
                {
                    var cbsId = gameModel.userName.ToLower().Replace("combosang", " ").Trim();
                    int cbsNum;
                    bool isSuccess = int.TryParse(cbsId, out cbsNum);
                    if (isSuccess)
                    {
                        if (cbsNum < int.Parse(StartComboSang))
                        {
                            return Ok(new { code = ResponseCode.ServerError, reply = "Tài khoản không hợp lệ" });
                        }
                    }
                    else
                    {
                        return Ok(new { code = ResponseCode.ServerError, reply = "Tài khoản không hợp lệ" });
                    }
                }
                else if (gameModel.gameTypeId == 4)
                {
                    var cbId = gameModel.userName.Replace("combo", " ").Trim();
                    int cbNum;
                    bool isSuccess = int.TryParse(cbId, out cbNum);
                    if (isSuccess)
                    {
                        if (cbNum < int.Parse(StartComboToi))
                        {
                            return Ok(new { code = ResponseCode.ServerError, reply = "Tài khoản không hợp lệ" });
                        }
                    }
                    else
                    {
                        return Ok(new { code = ResponseCode.ServerError, reply = "Tài khoản không hợp lệ" });
                    }
                }
            }

            DateTime currentDate = DateTime.Now;
            if (currentDate >= DateTime.Parse(EndGame))
            {
                return Ok(new { code = ResponseCode.ServerError, reply = "Chương trình đã kết thúc" });
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    GameResult gameResult = new GameResult();
                    gameResult.IsUsed = 0;
                    gameResult.UserName = gameModel.userName;
                    gameResult.ParamId = 9 + i;
                    gameResult.Code = Get8CharacterRandomString().ToUpper();
                    gameResult.GameId = gameModel.gameId;
                    if (gameModel.gameTypeId == 3)
                    { // Combo Sáng 
                    var currentDay = DateTime.Now.ToShortDateString() + " 11:00:00";
                        gameResult.ExpiratedDate = DateTime.Parse(currentDay);
                    }
                    else
                    { // Combo Tối
                        var currentDay = DateTime.Now.AddDays(1).ToShortDateString() + " 7:00:00";
                        gameResult.ExpiratedDate = DateTime.Parse(currentDay);
                    }
                    gameResult.CreatedDate = DateTime.Now;

                    _gameRepository.InsertEventResult(gameResult);
                }

                return Ok(new { code = ResponseCode.Success });
            }
        }

        private string Get8CharacterRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, 8);  // Return 8 character string
        }
    }
}
