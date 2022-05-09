using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayManagement.Models.GateWay.AccountGameMap
{
    [Table("AccountGameMap")]
    public class AccountGameMap
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int AccountId { get; set; }
        public int Round { get; set; }
    }
}
