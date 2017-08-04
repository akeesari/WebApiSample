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
  public class TestClient : TestBase
    {
       
        public void GetClients()
        {
                       
            var client = new OAuth2Client(new Uri(TokenEndpoint), ClientCredentials.ClientId, ClientCredentials.ClientSecret);
            
            //var additionalValues = new Dictionary<string, string>
            //{
            //    {"origin", applicationPath},
            //    {"sessionid", sessionId}
            //};
            TokenResponse tokenResponse;
            tokenResponse = client.RequestResourceOwnerPasswordAsync(null, null, null, null).Result;

            var tokenResponseObj = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse.Json.ToString());

            Console.WriteLine(tokenResponseObj.AccessToken);        
            Console.WriteLine(tokenResponseObj.TokenType );        
            Console.WriteLine(tokenResponseObj.Json);        
            Console.WriteLine(tokenResponseObj.ExpiresIn);        
        }

    }
}
