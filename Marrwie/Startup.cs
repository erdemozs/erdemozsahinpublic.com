using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Marrwie.Startup))]
namespace Marrwie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
