using SuperDuperMarkt.Data;
using SuperDuperMarkt.Data.ProductImports;
using SuperDuperMarkt.Data.Products;
using System;
using System.Collections.Generic;

namespace SuperDuperMarkt
{
    class Program
    {
        private const string CONSOLE_TITLE = "Super Duper Markt";

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = CONSOLE_TITLE;

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
                    new Cheese("Cheddar", 1.76f, 52, 50),
                    new Wine("Red Wine", 46f, 1)
                };
                range.ImportRangeList(products);
            }

            Console.WriteLine("\nStarting values:");
            range.PrintRangeForDate(DateTime.Now);
            Console.WriteLine("\n");
            for(int i = 1; i <= 30; i++)
            {
                range.PrintRangeForDate(DateTime.Now.AddDays(i));
            }
        }
    }
}
