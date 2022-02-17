using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Consumer
{
    public struct Product
    {
        public int id;
        public string type;
        public string name;
    }

    public class ProductClient
    {
        #nullable enable
        public async Task<List<Product>> GetProducts(string baseUrl, HttpClient? httpClient = null)
        {
            using var client = httpClient == null ? new HttpClient() : httpClient;

            var response = await client.GetAsync(baseUrl + "products");
            response.EnsureSuccessStatusCode();
            //return response;
            var responseStr = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Product>>(responseStr);
        }


        public async Task<Product> GetProduct(string baseUrl, string productId, HttpClient? httpClient = null)
        {
            using var client = httpClient == null ? new HttpClient() : httpClient;

            var response = await client.GetAsync(baseUrl + "/product/" + productId);
            response.EnsureSuccessStatusCode();

            var resp = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Product>(resp);
        }
    }
}