using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.Models.GateWay.Product
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { set; get; }
        public int? RecipeId { set; get; }
        public int ProductTypeId { set; get; }
        public string Name { set; get; }
        public string Avatar { set; get; }
        public string ShortDescription { set; get; }
        public string Description { set; get; }
        public decimal SalePrice { set; get; }
        public int ExpiryDate { set; get; }
        public bool HasAcceptCoin { set; get; }
        public bool IsActive { set; get; }

    }
}
