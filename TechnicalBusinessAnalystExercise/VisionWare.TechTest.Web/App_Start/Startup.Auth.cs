using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace VisionWare.TechTest.Web
{
    /// <summary>
    /// The <see cref="Startup"/> class.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/account/login")
            });
        }
    }
}