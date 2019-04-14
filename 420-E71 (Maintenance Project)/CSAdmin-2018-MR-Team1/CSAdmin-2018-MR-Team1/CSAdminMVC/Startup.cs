using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSAdminMVC.Startup))]
namespace CSAdminMVC {
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
