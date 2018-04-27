using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sustan.Startup))]
namespace Sustan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
