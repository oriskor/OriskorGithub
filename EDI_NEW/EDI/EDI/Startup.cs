using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EDI.Startup))]
namespace EDI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
