using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;
using System.Web.Script.Serialization;

namespace WebApiTest
{
  public class TestClient : TestBase
    {
       
        public void GetClient(int id)
        {
            string clientUrl = "api/client/getclient?id=" + id;

            using (var httpClient = GetHttpClient())
            {
                var response = httpClient.GetAsync(clientUrl).Result;

                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var client = response.Content.ReadAsAsync<Client>().Result;
                    Console.WriteLine("ClientId: {0}", client.ClientId);
                    
                }            
            }
        }
        public void GetClients()
        {
            const string clientUrl = "api/client/getclients";

            using (var httpClient = GetHttpClient())
            {
                var response = httpClient.GetAsync(clientUrl).Result;

                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var clients = response.Content.ReadAsAsync<List<Client>>().Result;
                    foreach (var client in clients)
                    {
                        Console.WriteLine("ClientId: {0}", client.ClientId);
                    }
                }
            }
        }
        public async Task AddClient()
        {

            const string clientUrl= "api/client";

            var client = new Client
            {
                Name = "ConsoleApp3",
                AllowedOrigin = "https://localhost:port/app",
                Secret = "abc@123",
                ApplicationType = ApplicationType.Console,
                Active = true,
                AllowedGrant = OAuthGrant.ResourceOwner,
                CreatedOn = DateTime.Now.ToShortDateString(),
            };
            //string jsonClient = new JavaScriptSerializer().Serialize(client);
            using (var httpClient = GetHttpClient())
            {
                var response =await httpClient.PostAsJsonAsync(clientUrl, client);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync();
                    var newClient =  JsonConvert.DeserializeObject<Client>(responseBody.Result);
                    Console.WriteLine("Client Name: {0}", client.Name);
                }
            }
        }
        public async Task UpdateClient(int id)
        {
            string clientUrl = "api/client/getclient?id=" + id;

            using (var httpClient = GetHttpClient())
            {
                var clientResponse = httpClient.GetAsync(clientUrl).Result;

                Client client;
                if (clientResponse.IsSuccessStatusCode)
                {
                    client = clientResponse.Content.ReadAsAsync<Client>().Result;
                    client.Active= false;
                    var postResponse = await httpClient.PutAsJsonAsync("api/client?name=" + client.Name, client);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        var responseBody = postResponse.Content.ReadAsStringAsync();
                        var newClient = JsonConvert.DeserializeObject<Client>(responseBody.Result);
                        Console.WriteLine("New Client Name: {0}", client.Active);
                    }
                }
            }            
        }
    }
}
