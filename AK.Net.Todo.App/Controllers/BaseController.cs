using System;
using System.Net.Http;
using System.Web.Mvc;
using System.Net.Http.Headers;
using Thinktecture.IdentityModel.Client;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace AK.Net.Todo.App.Controllers
{
    public abstract class BaseController : Controller
    {
        protected const string todoApiUrl = "api/todo";

        protected HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:38917/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Clear();
            return httpClient;
        }
        //protected HttpClient GetHttpClient()
        //{
        //    var httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("http://localhost:38917/");
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetAccessToken());
        //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    httpClient.DefaultRequestHeaders.Accept.Clear();
        //    return httpClient;
        //}

        protected string GetAccessToken()
        {
            const string tokenEndpoint = "http://localhost:38917/oauth/token";
            //Asuming you've already registered the client to  access the api. here you need to pass clientId and password.
            var client = new OAuth2Client(new Uri(tokenEndpoint), "66534b02-79c6-4283-b119-1b0f475a7b55", "AK.Net.Todo.App");
            const string clientAppPath = "http://localhost:39559/";

            var additionalValues = new Dictionary<string, string>
          {
              {"origin", clientAppPath},
          };

            var loggedInUser = User.Identity.GetUserName();
            var tokenResponse =
                client.RequestResourceOwnerPasswordAsync(loggedInUser, null, null, additionalValues).Result;
            var token = JsonConvert.DeserializeObject<Token>(tokenResponse.Json.ToString());
            return token.Access_Token;
        }
    }
    public class Token
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }

        public string Expires_In { get; set; }
        public string scope { get; set; }
        public string DhaUserId { get; set; }
        public string SessionId { get; set; }
        public string DhaUrl { get; set; }

    }
}