using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcScraper.Startup))]
namespace MvcScraper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
