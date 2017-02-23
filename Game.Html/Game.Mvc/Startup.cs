using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Game.Mvc.Startup))]
namespace Game.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
