using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayManagement.Models.GateWay.GameResult
{
    [Table("GameResult")]
    public class GameResult
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        [Computed]
        public string UserName { get; set; }
        public int GameId { get; set; }
        public int ParamId { get; set; }
        [Write(false)]
        [Computed]
        public string Name { get; set; }
        public string Code { get; set; }
        public int IsUsed { get; set; }
        public DateTime? CreatedDate { set; get; }
        public DateTime? ExpiratedDate { set; get; }
        public DateTime? ExchangeDate { set; get; }
        public int ExchangeBy { set; get; }
        public decimal Total { set; get; }
        public decimal Promotion { set; get; }
    }
}
