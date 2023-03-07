using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HospitalManagementSystem1.Startup))]
namespace HospitalManagementSystem1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
