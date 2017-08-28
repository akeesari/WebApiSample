using System;
using System.Net.Http;
using System.Web.Mvc;
using System.Net.Http.Headers;
namespace AK.Net.Todo.App.Controllers
{
    public abstract class BaseController : Controller
    {
        protected HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:38917/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Clear();
            return httpClient;
        }
    }
}