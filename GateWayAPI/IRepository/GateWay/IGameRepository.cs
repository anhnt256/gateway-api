using GateWayManagement.Models.GateWay.Game;
using GateWayManagement.Models.GateWay.GameParam;
using GateWayManagement.Models.GateWay.GameResult;
using GateWayManagement.Models.GateWay.AccountGameMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWayAPI.Models.General.GameResultModel;
using GateWayAPI.Models.GateWay.GameResultDetail;

namespace GateWayAPI.IRepository.GateWay
{
    public interface IGameRepository
    {
        List<Game> SelectAllGames();
        List<GameParamClient> SelectAllGameParamClient(int gameId);
        List<GameParam> SelectAllGameParam(int gameId);
        long InsertUserGameMap(AccountGameMap userGameMap);
        Task<int> CheckRound(int accountId, int gameId);
        Task<AccountGameMap> CheckExist(int accountId, int gameId);
        int UpdateRound(int accountId, int gameId, int? round);
        int CalcRound(int accountId, int gameId);
        long InsertEventResult(GameResult result);
        List<GameResult> GetResult(int gameId, int accountId);
        List<GameResult> GetServerResult(int gameId);
        GameResultModel GetResultByCode(string code);
        bool UseCode(GameResultModel gameResult);
        void InsertGameResultDetail(GameResultDetail detail);
    }
}
