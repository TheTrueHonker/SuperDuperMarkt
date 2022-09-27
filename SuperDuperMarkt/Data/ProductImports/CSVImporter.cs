using SuperDuperMarkt.Data.Products;
using System;
using System.Collections.Generic;
using System.IO;

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

        /// <summary>
        /// Creats an instance of the class <see cref="CSVImporter"/> by asking the user for a file path via console.
        /// Asks as long as a path with a valid file is entered. 
        /// </summary>
        /// <returns>An instance of <see cref="CSVImporter"/> with a valid path.</returns>
        public static CSVImporter CreateCSVImporterWithDialog()
        {
            string path;
            do
            {
                Console.WriteLine("Please enter a valid path to a csv-file:");
                path = Console.ReadLine();
                if (!File.Exists(path))
                    Console.WriteLine($"File does not exist: \"{path}\"");
                if (Path.GetExtension(path) != ".csv")
                    Console.WriteLine("File must have an extension of \".csv\"");
            } while (!File.Exists(path));
            return new CSVImporter(path);
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

        /// <summary>
        /// Converts a line from a valid csv-file to a <see cref="Product"/> object
        /// </summary>
        /// <param name="line">A singular data line from the csv-file</param>
        /// <returns>A child of <see cref="Product"/></returns>
        private Product ConvertLineToProduct(string line)
        {
            var parameters = GetLineParameters(line);
            var description = parameters.GetValueOrDefault(CSV_Parameter.Description, "");
            float.TryParse(parameters.GetValueOrDefault(CSV_Parameter.FixPrice, "0"), out var fixPrice);
            int.TryParse(parameters.GetValueOrDefault(CSV_Parameter.Quality), out var quality);
            int.TryParse(parameters.GetValueOrDefault(CSV_Parameter.DueInDays), out var dueInDays);
            DateTime.TryParse(parameters.GetValueOrDefault(CSV_Parameter.DueDate), out var dueDate);
            DateTime dateCreated = DateTime.Now;
            if (parameters.GetValueOrDefault(CSV_Parameter.DateCreated,"") != "")
            {
                dateCreated = DateTime.Parse(parameters.GetValueOrDefault(CSV_Parameter.DateCreated));
            }
            switch (parameters[(int)CSV_Parameter.Type])
            {
                case "Cheese":
                    return new Cheese(description, fixPrice, quality, dueInDays, dateCreated);
                case "Wine":
                    return new Wine(description, fixPrice, quality, dateCreated);
                case "Bread":
                    return new Bread(description, fixPrice, quality, dueDate, dateCreated);
                default:
                    Console.WriteLine($"Unsupported product: {parameters.GetValueOrDefault(CSV_Parameter.Type)}");
                    return null;
            }
        }

        /// <summary>
        /// Gets all parameters from a singular line of a csv-file
        /// </summary>
        /// <param name="line">A singular line from the csv-file</param>
        /// <returns>A <see cref="Dictionary{CSV_Parameter, string}"/> with all parameters</returns>
        private Dictionary<CSV_Parameter, string> GetLineParameters(string line)
        {
            var dict = new Dictionary<CSV_Parameter, string>();
            string parameter = "";
            int currentParameter = 0;
            bool ignoreComma = false;
            foreach(char character in line)
            {
                if (character == '"')
                {
                    ignoreComma = !ignoreComma;
                }
                else
                {
                    if (character == ',' && !ignoreComma)
                    {
                        dict.Add((CSV_Parameter)currentParameter, parameter);
                        parameter = "";
                        currentParameter++;
                    }
                    else
                    {
                        parameter += character;
                    }
                }
            }
            dict.Add((CSV_Parameter)currentParameter, parameter);
            return dict;
        }
    }

    enum CSV_Parameter{
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
