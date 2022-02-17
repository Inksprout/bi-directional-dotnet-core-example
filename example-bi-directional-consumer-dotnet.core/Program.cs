using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUri = "http://localhost:9000";

            Console.WriteLine("Fetching products");
            var consumer = new ProductClient();
           var result = consumer.GetProducts(baseUri).GetAwaiter().GetResult();
            Console.WriteLine(result);

            Product productResult = consumer.GetProduct(baseUri, "20").GetAwaiter().GetResult();
            Console.WriteLine(productResult);
        }

        static private void WriteoutArgsUsed(string datetimeArg, string baseUriArg)
        {
            Console.WriteLine($"Running consumer with args: dateTimeToValidate = {datetimeArg}, baseUri = {baseUriArg}");
        }

        static private void WriteoutUsageInstructions()
        {
            Console.WriteLine("To use with your own parameters:");
            Console.WriteLine("Usage: dotnet run ");
            Console.WriteLine("Usage Example: dotnet run 01/01/2018 http://localhost:9000");
        }
    }
}