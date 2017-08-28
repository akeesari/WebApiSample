using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

//[assembly: OwinStartup(typeof(AK.Net.Todo.Api.Startup))]
[assembly: OwinStartup("ApiConfig", typeof(AK.Net.Todo.Api.Startup))]
namespace AK.Net.Todo.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureAuth(app);
            WebApiConfig.Register(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<DhaIdentityContext, Configuration>());
        }
    }
}
