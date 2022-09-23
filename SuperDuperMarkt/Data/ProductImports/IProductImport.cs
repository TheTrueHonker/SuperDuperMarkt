using SuperDuperMarkt.Data.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperDuperMarkt.Data.ProductImports
{
    public interface IProductImport
    {
        List<Product> GetProducts(); 
    }
}
