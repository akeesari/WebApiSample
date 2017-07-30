using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AK.Net.Todo.App.Startup))]
namespace AK.Net.Todo.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
