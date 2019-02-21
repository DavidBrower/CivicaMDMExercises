using Microsoft.Owin;
using Owin;
using VisionWare.TechTest.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace VisionWare.TechTest.Web
{
    /// <summary>
    /// The <see cref="Startup"/> class.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
