using System;
using System.Collections.Generic;
using System.Text;
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

        public static ProductRange GetProductRange()
        {
            if (productRange == null)
                productRange = new ProductRange();
            return productRange;
        }

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

        public void ImportRangeList(List<Product> newProducts)
        {
            range = newProducts;
        }
    }
}
