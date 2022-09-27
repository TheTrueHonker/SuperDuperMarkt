using SuperDuperMarkt.Data.Products;
using System.Collections.Generic;

namespace SuperDuperMarkt.Data.ProductImports
{
    public interface IProductImport
    {
        /// <summary>
        /// Loads and returns the products from this interface.
        /// </summary>
        /// <returns>A list with the loaded products</returns>
        List<Product> GetProducts(); 
    }
}
