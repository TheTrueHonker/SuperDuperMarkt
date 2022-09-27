using SuperDuperMarkt.Data.Products;
using System.Collections.Generic;

namespace SuperDuperMarkt.Data.ProductImports
{
    public interface IProductImport
    {
        List<Product> GetProducts(); 
    }
}
