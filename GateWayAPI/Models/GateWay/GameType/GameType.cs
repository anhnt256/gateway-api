using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayManagement.Models.GateWay.GameType
{
    [Table("GameType")]
    public class GameType
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal MoneyPerRound { set; get; }
        public string Description { set; get; }
    }
}
