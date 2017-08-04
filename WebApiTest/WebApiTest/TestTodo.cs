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
  public class TestTodo : TestBase
    {
       
        public void TestTodoApi()
        {
            //var tokenEndpoint = GetAuthorizationServerBaseAddress();
            //var clientCredentials = GetClientCredentials();
            var client = new OAuth2Client(new Uri(TokenEndpoint), ClientCredentials.ClientId, ClientCredentials.ClientSecret);
            
            //TODO:AK Update client application path and SessionId for more constraints
            var applicationPath = string.Empty;
            var sessionId = string.Empty;
            //if (HttpContext.Session != null)
            //{
            //    sessionId = HttpContext.Session.SessionID;
            //}
            //if (HttpContext.Request.Url != null)
            //{
            //    applicationPath = HttpContext.Request.Url.AbsoluteUri;
            //}

            var additionalValues = new Dictionary<string, string>
            {
                {"origin", applicationPath},
                {"sessionid", sessionId}
            };
            TokenResponse tokenResponse;
            tokenResponse = client.RequestResourceOwnerPasswordAsync(null, null, null, additionalValues).Result;

            var tokenResponseObj = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse.Json.ToString());
            Console.WriteLine(tokenResponseObj.AccessToken);        
            Console.WriteLine(tokenResponseObj.TokenType );        
            Console.WriteLine(tokenResponseObj.Json);        
            Console.WriteLine(tokenResponseObj.ExpiresIn);        
        }

    }
}
