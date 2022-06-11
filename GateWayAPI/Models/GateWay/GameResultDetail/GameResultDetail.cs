using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.GameResultDetail
{
    [Table("GameResultDetail")]
    public class GameResultDetail
    {
        [Key]
        public int Id { set; get; }
        public int GameResultId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal SalePrice { set; get; }
    }
}
