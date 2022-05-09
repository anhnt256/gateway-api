using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayManagement.Models.GateWay.GameParam
{
    [Table("GameParam")]
    public class GameParam
    {
        [Key]
        public int Id { set; get; }
        public int GameId { set; get; }
        public int GameTypeId { set; get; }
        public string Name { set; get; }
        public decimal Rate { set; get; }
        public int Expiry { set; get; }
        public DateTime FromTime { set; get; }
        public DateTime ToTime { set; get; }
    }
}
