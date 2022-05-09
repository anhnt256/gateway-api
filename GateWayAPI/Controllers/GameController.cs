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
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        [Route("update-round")]
        public IActionResult UpdateRound([FromBody] GameModel gameModel)
        {
            int accountId = CheckLogin(gameModel.ip, gameModel.accountId);
            if (accountId == 0)
            {
                return Ok(new { code = ResponseCode.RequiredLogin, reply = "" });
            }
            var totalAmount = _paymentRepository.GetTotalPaymentByUserId(accountId);
            var usedRound = _gameRepository.GetResult(gameModel.gameId, accountId).Count;
            int totalRound = (int)totalAmount / 50000;
            int currentRound = totalRound - usedRound;
            _gameRepository.UpdateRound(accountId, gameModel.gameId, currentRound);

            return Ok(new { code = ResponseCode.Success, reply = currentRound });
        }

        [HttpPost]
        [Route("calc-prize")]
        public IActionResult CalcPrize([FromBody] GameModel gameModel)
        {
            int accountId = CheckLogin(gameModel.ip, gameModel.accountId);
            if (accountId == 0)
            {
                return Ok(new { code = ResponseCode.RequiredLogin, reply = "" });
            }
            var round = _gameRepository.CheckRound(accountId, gameModel.gameId).Result;
            if (round == 0)
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
            gameResult.Code = Guid.NewGuid().ToString();
            gameResult.GameId = gameModel.gameId;
            gameResult.ExpiratedDate = DateTime.Now.AddDays(gameParams[result].Expiry);
            gameResult.CreatedDate = DateTime.Now;

            _gameRepository.InsertEventResult(gameResult);
            _gameRepository.UpdateRound(accountId, gameModel.gameId, round - 1);

            return Ok(new { code = ResponseCode.Success, reply = new { result, round = round - 1 } });
        }

        [HttpPost]
        [Route("prize")]
        public ActionResult GetResult([FromBody] GameModel gameModel)
        {
            int accountId = CheckLogin(gameModel.ip, gameModel.accountId);
            if (accountId == 0)
            {
                return Ok(new { code = ResponseCode.RequiredLogin, reply = "" });
            }
            var eventResult = _gameRepository.GetResult(gameModel.gameId, accountId);

            return Ok(new { code = ResponseCode.Success, reply = eventResult });
        }

        [HttpPost]
        [Route("init-game")]
        public ActionResult InitGame([FromBody] GameModel gameModel)
        {
            int accountId = CheckLogin(gameModel.ip, gameModel.accountId);
            if (accountId == 0)
            {
                return Ok(new { code = ResponseCode.RequiredLogin, reply = "" });
            }

            var game = _gameRepository.CheckExist(accountId, gameModel.gameId).Result;
            if (game is null)
            {
                var totalAmount = _paymentRepository.GetTotalPaymentByUserId(accountId);
                var usedRound = _gameRepository.GetResult(gameModel.gameId, accountId).Count;
                int totalRound = (int)totalAmount / 50000;
                int currentRound = totalRound - usedRound;

                AccountGameMap userGameMap = new AccountGameMap();
                userGameMap.GameId = gameModel.gameId;
                userGameMap.AccountId = accountId;
                userGameMap.Round = int.Parse(currentRound.ToString());
                var eventResult = _gameRepository.InsertUserGameMap(userGameMap);

                return Ok(new { code = ResponseCode.Success, reply = eventResult });
            }
            else {
                return Ok(new { code = ResponseCode.Success, reply = "" });
            }
        }

        private int CheckLogin(string ip, int userId)
        {
            var computer = _accountRepository.GetRealIpFromLocalIp(ip);
            if (computer != null)
            {
                User user = _userRepository.GetUserByIPAsync(computer.RealIp).Result;
                if (user is null || user.UserId != userId)
                {
                    return 0;
                }
                else
                {
                    return user.UserId;
                }
            }
            return 0;
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
    }
}
