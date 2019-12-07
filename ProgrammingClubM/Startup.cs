using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProgrammingClubM.Startup))]
namespace ProgrammingClubM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
