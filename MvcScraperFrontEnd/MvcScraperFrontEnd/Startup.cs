using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcScraperFrontEnd.Startup))]
namespace MvcScraperFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
