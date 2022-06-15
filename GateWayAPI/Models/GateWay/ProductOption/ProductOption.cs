using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.ProductOption
{
    [Table("ProductOption")]
    public class ProductOption
    {
        [Key]
        public int Id { set; get; }
        public int ProductId { set; get; }
        public string Name { set; get; }
        public string Avatar { set; get; }
        public int Min { set; get; }
        public int Max { set; get; }
        public double SalePrice { set; get; }
        public bool IsDefault { set; get; }
        public bool IsMultiple { set; get; }
        public bool IsRequire { set; get; }
        [Computed]
        public int Quantity { set; get; }
    }
}
