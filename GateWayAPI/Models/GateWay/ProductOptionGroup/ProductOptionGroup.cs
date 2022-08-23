using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.ProductOptionGroup
{
    [Table("ProductOptionGroup")]
    public class ProductOptionGroup
    {
        [Key]
        public int Id { set; get; }
        public int ProductId { set; get; }
        public string Name { set; get; }
        public bool IsActive { set; get; }
        public bool IsRequired { set; get; }
        public bool IsMultiple { set; get; }
    }
}
