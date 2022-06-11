using GateWayAPI.IRepository;
using GateWayAPI.IRepository.CSM;
using GateWayAPI.IRepository.GateWay;
using GateWayAPI.Models.CSM.User;
using GateWayAPI.Models.GateWay.General;
using GateWayAPI.Models.GateWay.General.GameModel;
using GateWayManagement.Models;
using GateWayManagement.Models.GateWay.AccountGameMap;
using GateWayManagement.Models.GateWay.GameParam;
using GateWayManagement.Models.GateWay.GameResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GateWayAPI.Controllers
{
    [Route("game")]
    public class GameController : GlobalController
    {
        private IUserRepository _userRepository;
        private IAccountRepository _accountRepository;
        private IPaymentRepository _paymentRepository;
        private IGameRepository _gameRepository;

        public GameController(
            IPaymentRepository paymentRepository,
            IUserRepository userRepository,
            IGameRepository gameRepository,
            IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
            _gameRepository = gameRepository;
            _accountRepository = accountRepository;
        }

        string MoneyPerRound = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Config")["MoneyPerRound"];

        [Authorize]
        [HttpPost]
        [Route("update-round")]
        public IActionResult UpdateRound([FromBody] GameModel gameModel)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            int accountId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
            if (accountId == 0)
            {
                return Ok(new { code = ResponseCode.RequiredLogin, reply = "" });
            }
            var totalAmount = _paymentRepository.GetTotalPaymentByUserId(accountId);
            var usedRound = _gameRepository.GetResult(gameModel.gameId, accountId).Count;
            int totalRound = (int)totalAmount / int.Parse(MoneyPerRound);
            int currentRound = totalRound - usedRound;
            if (currentRound < 0)
            {
                currentRound = 0;
            }
            _gameRepository.UpdateRound(accountId, gameModel.gameId, currentRound);

            return Ok(new { code = ResponseCode.Success, reply = currentRound });
        }

        [Authorize]
        [HttpPost]
        [Route("calc-prize")]
        public IActionResult CalcPrize([FromBody] GameModel gameModel)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            int accountId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
            if (accountId == 0)
            {
                return Ok(new { code = ResponseCode.RequiredLogin, reply = "" });
            }

            var totalAmount = _paymentRepository.GetTotalPaymentByUserId(accountId);
            var usedRound = _gameRepository.GetResult(gameModel.gameId, accountId).Count;
            int totalRound = (int)totalAmount / int.Parse(MoneyPerRound);
            int currentRound = totalRound - usedRound;
            if (currentRound < 0)
            {
                currentRound = 0;
            }
            if (currentRound == 0)
            {
                return Ok(new { code = ResponseCode.ParamsInvalid, reply = "" });
            }
            var gameParams = _gameRepository.SelectAllGameParam(gameModel.gameId);
            var items = GenerationItems(gameParams);
            Random random = new Random();
            var result = items[random.Next(items.Length)];

            GameResult gameResult = new GameResult();
            gameResult.IsUsed = 0;
            gameResult.AccountId = accountId;
            gameResult.ParamId = result;
            gameResult.Code = Get8CharacterRandomString().ToUpper();
            gameResult.GameId = gameModel.gameId;
            gameResult.ExpiratedDate = DateTime.Now.AddDays(gameParams[result].Expiry);
            gameResult.CreatedDate = DateTime.Now;

            _gameRepository.InsertEventResult(gameResult);
            _gameRepository.UpdateRound(accountId, gameModel.gameId, currentRound - 1);

            return Ok(new { code = ResponseCode.Success, reply = new { result, round = currentRound - 1 } });
        }

        [Authorize]
        [HttpPost]
        [Route("prize")]
        public ActionResult GetResult([FromBody] GameModel gameModel)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            int accountId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
            if (accountId == 0)
            {
                return Ok(new { code = ResponseCode.RequiredLogin, reply = "" });
            }
            var eventResult = _gameRepository.GetResult(gameModel.gameId, accountId);

            return Ok(new { code = ResponseCode.Success, reply = eventResult });
        }

        [Authorize]
        [HttpPost]
        [Route("init-game")]
        public ActionResult InitGame([FromBody] GameModel gameModel)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            int accountId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
            if (accountId == 0)
            {
                return Ok(new { code = ResponseCode.RequiredLogin, reply = "" });
            }

            var game = _gameRepository.CheckExist(accountId, gameModel.gameId).Result;
            if (game is null)
            {
                var totalAmount = _paymentRepository.GetTotalPaymentByUserId(accountId);
                var usedRound = _gameRepository.GetResult(gameModel.gameId, accountId).Count;
                int totalRound = (int)totalAmount / int.Parse(MoneyPerRound);
                int currentRound = totalRound - usedRound;
                if (currentRound < 0)
                {
                    currentRound = 0;
                }

                AccountGameMap userGameMap = new AccountGameMap();
                userGameMap.GameId = gameModel.gameId;
                userGameMap.AccountId = accountId;
                userGameMap.Round = int.Parse(currentRound.ToString());
                var eventResult = _gameRepository.InsertUserGameMap(userGameMap);

                return Ok(new { code = ResponseCode.Success, reply = eventResult });
            }
            else
            {
                return Ok(new { code = ResponseCode.Success, reply = "" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("params")]
        public ActionResult GetGameParams(int gameId)
        {
            var eventResult = _gameRepository.SelectAllGameParamClient(gameId);

            return Ok(new { code = ResponseCode.Success, reply = eventResult });
        }

        private int[] GenerationItems(List<GameParam> gameParams)
        {
            int[] items = new int[10000];
            int index = 0;
            foreach (var item in gameParams)
            {
                for (int i = 0; i < item.Rate * 100; i++)
                {
                    items[index] = item.Id;
                    index++;
                }
            }
            return items;
        }

        private string Get8CharacterRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, 8);  // Return 8 character string
        }
    }
}
