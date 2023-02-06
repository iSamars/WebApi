using UserApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace UserApp.Helpers
{
    public static class RequestHelper
    {
        public async static void Create(Product product)
        {
            using (var client = new HttpClient())
            {
                await client.PostAsync(
                    Config.host,
                    new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json"));
            }
        }

        public static async Task<List<Product>> Get()
        {         
            using (var client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(Config.host);
                return await resp.Content.ReadAsAsync<List<Product>>();
            }
        }

        public async static void Update(Product product)
        {
            using (var client = new HttpClient())
            {
                string myJson = JsonConvert.SerializeObject(product);

                await client.PutAsync(
                        Config.host,
                        new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json"));
            }
        }

        public async static void Delete(int id)
        {
            using (var client = new HttpClient())
            {
                await client.DeleteAsync(
                    $"{Config.host}?id={id}");
            }
        }
    }
}
