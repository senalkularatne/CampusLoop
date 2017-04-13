using Microsoft.Owin;
using Owin;

namespace UCEvents_WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
            ConfigureDatabase(app);
        }
    }
}