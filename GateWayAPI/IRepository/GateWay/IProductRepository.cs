using GateWayAPI.Models.GateWay.Account;
using GateWayAPI.Models.GateWay.Computer;
using GateWayAPI.Models.GateWay.Product;
using GateWayAPI.Models.GateWay.ProductOption;
using GateWayAPI.Models.GateWay.ProductType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayAPI.IRepository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProduct();
        IEnumerable<Product> GetProductByType(int ProductType);
        IEnumerable<ProductType> GetAllProductType();
        IEnumerable<ProductOption> GetProductOptionByProductId(int ProductId);

    }
}
