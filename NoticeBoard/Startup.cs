using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NoticeBoard.Startup))]
namespace NoticeBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
