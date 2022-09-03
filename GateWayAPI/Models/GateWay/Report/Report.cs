using Dapper.Contrib.Extensions;
using GateWayAPI.Models.GateWay.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.Report
{
    [Table("Report")]
    public class Report
    {
        [Key]
        public int Id { set; get; }
        public int StaffId { set; get; }
        public int CashierId { set; get; }
        public int ShiftId { set; get; }
        public int HandOverId { set; get; }
        public double TimeMoney { set; get; }
        public double FoodMoney { set; get; }
        public double Esale { set; get; }
        public double ComboMoney { set; get; }
        public double TotalMoney { set; get; }
        public double OtherMoney { set; get; }
        public string Note { set; get; }
        public int TotalAccount { set; get; }
        public DateTime ReportDate { set; get; }
        public string UploadESaleMoney { set; get; }
        [Computed]
        public List<Orders> Orders { set; get; }
        [Computed]
        public string OrderJSON { set; get; }
        [Computed]
        public List<OrderDetail.OrderDetail> OrderDetails { set; get; }
        [Computed]
        public string OrderDetailJSON { set; get; }
    }
}
