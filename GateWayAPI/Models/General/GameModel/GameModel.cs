using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.General.GameModel
{
    public class GameModel
    {
        public int accountId { get; set; }
        public int gameId { get; set; }
        public int gameTypeId { get; set; }
        public string userName { get; set; }
        public string ip { get; set; }
        public string code { get; set; }

    }
}
