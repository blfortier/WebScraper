using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcSeleniumScraper.Startup))]
namespace MvcSeleniumScraper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
