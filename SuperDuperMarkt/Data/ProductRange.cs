using System;
using System.Collections.Generic;
using SuperDuperMarkt.Data.ProductImports;
using SuperDuperMarkt.Data.Products;

namespace SuperDuperMarkt.Data
{
    public class ProductRange
    {
        private static ProductRange productRange;
        private List<Product> range;

        private ProductRange()
        {
            range = new List<Product>();
        }

        /// <summary>
        /// By using the singleton pattern this static function return a <see cref="ProductRange"/>.
        /// The singleton pattern is used since a singular market can only have a singular range of products in its shelfs.
        /// </summary>
        /// <returns>A <see cref="ProductRange"/> by using the singleton patterns</returns>
        public static ProductRange GetProductRange()
        {
            if (productRange == null)
                productRange = new ProductRange();
            return productRange;
        }

        /// <summary>
        /// Prints all products for a specific <see cref="DateTime"/>.
        /// Products with a bad quality are printed red.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> for which the products should be printed.</param>
        public void PrintRangeForDate(DateTime dateTime)
        {
            Console.WriteLine($"Range for {dateTime:dd.MM.yyyy}");
            Console.WriteLine("---------------------");
            foreach(Product product in range)
            {
                product.UpdateQuality(dateTime);
                if(!product.IsQualityGood())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(product.ToString());
                    Console.WriteLine(" (Must be disposed of)");
                    Console.ForegroundColor = ConsoleColor.Gray;

                } else
                {
                    Console.WriteLine(product.ToString());
                }
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// Imports new products from a list of products and overrides the old products with them.
        /// </summary>
        /// <param name="newProducts">The new products that are to be imported</param>
        public void ImportRangeList(List<Product> newProducts)
        {
            range = newProducts;
        }

        /// <summary>
        /// Imports new products from a <see cref="IProductImport"/> by using <see cref="IProductImport.GetProducts"/>.
        /// Old products are overwritten by the new ones.
        /// </summary>
        /// <param name="productImport">The class which imports the new products.</param>
        public void ImportRangeList(IProductImport productImport)
        {
            range = productImport.GetProducts();
        }
    }
}
