using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FavoriteSongs.Startup))]
namespace FavoriteSongs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
