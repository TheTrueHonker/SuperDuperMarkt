using SuperDuperMarkt.Data.Products;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SuperDuperMarkt.Data.ProductImports
{
    class CSVImporter : IProductImport
    {
        private const string CSV_HEADER = "Type,Description,Quality,DueDate,DueInDays,FixPrice,DailyQualityModifier,DateCreated";

        private string path;


        public CSVImporter(string path)
        {
            this.path = path;
        }

        public List<Product> GetProducts()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                return new List<Product>();
            }

            var lines = File.ReadAllLines(path);

            bool firstLine = true;
            var products = new List<Product>();
            foreach(var line in lines)
            {
                // Check header
                if(firstLine)
                {
                    if (!line.Equals(CSV_HEADER))
                    {
                        Console.WriteLine($"CSV-Header is wrong!\nExpected header: {CSV_HEADER}");
                        return new List<Product>();
                    }
                    firstLine = false;
                    continue;
                }

                var product = ConvertLineToProduct(line);
                if (product != null)
                    products.Add(product);
            }
            return products;
        }

        private Product ConvertLineToProduct(string line)
        {
            var parameters = line.Split(',');
            DateTime dateCreated = DateTime.Now;
            switch(parameters[(int)CSV_Parameters.Type])
            {
                case "Cheese":
                    if(parameters[(int)CSV_Parameters.DateCreated] != "")
                    {
                        dateCreated = DateTime.Parse(parameters[(int)CSV_Parameters.DateCreated]);
                    }
                    return new Cheese(
                        parameters[((int)CSV_Parameters.Description)],
                        ((float)double.Parse(parameters[(int)CSV_Parameters.FixPrice], CultureInfo.InvariantCulture)),
                        int.Parse(parameters[(int)CSV_Parameters.Quality]),
                        int.Parse(parameters[(int)CSV_Parameters.DueInDays]),
                        dateCreated);
                case "Wine":
                    if (parameters[((int)CSV_Parameters.DateCreated)] != "")
                    {
                        dateCreated = DateTime.Parse(parameters[(int)CSV_Parameters.DateCreated]);
                    }
                    return new Wine(
                        parameters[((int)CSV_Parameters.Description)],
                        ((float)double.Parse(parameters[(int)CSV_Parameters.FixPrice], CultureInfo.InvariantCulture)),
                        int.Parse(parameters[(int)CSV_Parameters.Quality]),
                        dateCreated);
                default:
                    Console.WriteLine($"Unsupported product: {parameters[(int)CSV_Parameters.Type]}");
                    return null;
            }
        }
    }

    enum CSV_Parameters{
        Type = 0,
        Description = 1,
        Quality = 2,
        DueDate = 3,
        DueInDays = 4,
        FixPrice = 5,
        DailyQualityModifier = 6,
        DateCreated = 7
    }
}
