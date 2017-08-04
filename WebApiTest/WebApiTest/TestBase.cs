using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
