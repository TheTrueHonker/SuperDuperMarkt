using SuperDuperMarkt.Data;
using SuperDuperMarkt.Data.ProductImports;
using SuperDuperMarkt.Data.Products;
using System;
using System.Collections.Generic;

namespace SuperDuperMarkt
{
    class Program
    {
        static void Main()
        {
            var range = ProductRange.GetProductRange();
            Console.WriteLine("Import from CSV-File? (y/n)");
            if(Console.ReadLine().ToLower() == "y")
            {
                CSVImporter importer = CSVImporter.CreateCSVImporterWithDialog();
                range.ImportRangeList(importer);
            } else
            {
                var products = new List<Product>
                {
                    new Cheese("Cheddar", 1.76f, 31, 50),
                    new Wine("Red Wine", 46f, 1)
                };
                range.ImportRangeList(products);
            }
            
            range.PrintRangeForDate(DateTime.Now);
            for(int i = 1; i <= 5; i++)
            {
                range.PrintRangeForDate(DateTime.Now.AddDays(i));
            }
        }
    }
}
