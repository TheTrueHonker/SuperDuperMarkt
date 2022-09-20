using SuperDuperMarkt.Data;
using SuperDuperMarkt.Data.Products;
using System;
using System.Collections.Generic;

namespace SuperDuperMarkt
{
    class Program
    {
        static void Main(string[] args)
        {
            var products = new List<Product>
            {
                new Cheese("Cheddar", 1.76f, 31, 50),
                new Wine("Red Wine", 46f, 1)
            };

            var range = ProductRange.GetProductRange();
            range.ImportRangeList(products);
            range.PrintRangeForDate(DateTime.Now);
            for(int i = 1; i <= 5; i++)
            {
                range.PrintRangeForDate(DateTime.Now.AddDays(i));
            }
            
        }
    }
}
