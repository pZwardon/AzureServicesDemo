using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AzureServicesDemo.Startup))]
namespace AzureServicesDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
