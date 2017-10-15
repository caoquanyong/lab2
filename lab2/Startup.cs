using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab2.Startup))]
namespace lab2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
