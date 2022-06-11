using GateWayAPI.Models.GateWay.GameResultDetail;
using GateWayManagement.Models.GateWay.GameParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.General.GameResultModel
{
    public class GameResultModel
    {
        public int GameResultId { set; get; }
        public string Code { set; get; }
        public bool IsUsed { set; get; }
        public int Id { set; get; }
        public int ProductTypeId { set; get; }
        public string Description { set; get; }
        public string Name { get; set; }
        public bool IsActive { set; get; }
        public decimal MinOrder { set; get; }
        public decimal MaxPromotion { set; get; }
        public int PromotionType { set; get; }
        public int PromotionValue { set; get; }
        public string ProductApply { set; get; }
        public DateTime? CreatedDate { set; get; }
        public DateTime? ExpiratedDate { set; get; }
        public DateTime? ExchangeDate { set; get; }
        public int ExchangeBy { set; get; }
        public decimal Total { set; get; }
        public decimal Promotion { set; get; }
        public List<GameResultDetail> Detail { set; get; }
    }
}
