using System;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;
using Microsoft.Owin;
using System.Net.Http.Formatting;
using Microsoft.Owin.Security.OAuth;
using System.Linq;
using Newtonsoft.Json.Serialization;
using Microsoft.Azure.Mobile.Server.Tables.Config;
using Microsoft.Owin.Security;
using Microsoft.Azure.Mobile.Server.Authentication;
using System.Configuration;
using UCEvents_WebAPI.Infrastructure;
using UCEvents_WebAPI.Providers;

namespace UCEvents_WebAPI
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            ConfigureOAuthTokenGeneration(app);

            ConfigureWebApi(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);
            ConfigureSwagger(config);
        }

        private static void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationUserContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // Plugin the OAuth bearer JSON Web Token tokens generation and Consumption will be here
            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                Provider = new ApplicationOAuthProvider()
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthBearerTokens(oAuthServerOptions);
        }

        private static void ConfigureWebApi(HttpConfiguration config)
        {
            //Attribute Routing
            config.MapHttpAttributeRoutes();

            //Convention-based routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
              );
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}