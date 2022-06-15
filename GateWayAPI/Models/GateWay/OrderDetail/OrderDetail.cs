using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.OrderDetail
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int Id { set; get; }
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public string Name { set; get; }
        public string Avatar { set; get; }
        public int Quantity { set; get; }
        public decimal SalePrice { set; get; }
        public string JsonOptions { set; get; }
        [Computed]
        public List<ProductOption.ProductOption> Options { set; get; }
    }
}
