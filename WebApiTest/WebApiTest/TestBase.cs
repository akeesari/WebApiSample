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

namespace WebApiTest
{
  public class TestBase
    {
        public string TokenEndpoint { get; set; }
        public ClientCredentials ClientCredentials { get; set; }

        public  void GetAuthorizationServerBaseAddress()
        {
            
            if (ConfigurationManager.AppSettings.Count > 0)
            {
                TokenEndpoint = ConfigurationManager.AppSettings["tokenendpoint"];
            }
            
        }
        protected HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:38917/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Clear();
            
            return httpClient;


        }
        protected  void GetClientCredentials()
        {
            var clientCredentials = new ClientCredentials
            {
                ClientId = ConfigurationManager.AppSettings["clientid"],
                ClientSecret = ConfigurationManager.AppSettings["clientsecret"]
            };

            ClientCredentials= clientCredentials;
        }      
    }
}
