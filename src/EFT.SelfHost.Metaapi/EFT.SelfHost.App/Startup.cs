using Autofac.Core;
using Autofac.Integration.WebApi;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using Nancy;
using Autofac;
using Autofac.Builder;
using Microsoft.AspNet.Identity;
using AspNet.Identity.MongoDB;
using EFT.Meta.SelfHost.Api.Entities;
using EFT.Meta.SelfHost.Api.Providers;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Infrastructure;

namespace EFT.Meta.SelfHost.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            var builder = new ContainerBuilder();

            builder.RegisterType<MongoContext>().AsImplementedInterfaces<MongoContext, ConcreteReflectionActivatorData>().SingleInstance();

            builder.RegisterType<AuthRepository>().SingleInstance();

            builder.RegisterType<ApplicationIdentityContext>()
               .SingleInstance();

            builder.RegisterType<UserStore<User>>()
                .AsImplementedInterfaces<IUserStore<User>, ConcreteReflectionActivatorData>()
                .SingleInstance();

            builder.RegisterType<RoleStore<Role>>()
                .AsImplementedInterfaces<IRoleStore<Role>, ConcreteReflectionActivatorData>()
                .SingleInstance();

            builder.RegisterType<ApplicationUserManager>()
                .SingleInstance();

            builder.RegisterType<ApplicationRoleManager>()
                .SingleInstance();

            builder.RegisterType<SimpleAuthorizationServerProvider>()
                .AsImplementedInterfaces<IOAuthAuthorizationServerProvider, ConcreteReflectionActivatorData>().SingleInstance();

            builder.RegisterType<SimpleRefreshTokenProvider>()
                .AsImplementedInterfaces<IAuthenticationTokenProvider, ConcreteReflectionActivatorData>().SingleInstance();

            builder.RegisterApiControllers(typeof(Startup).Assembly);

            var container = builder.Build();

            app.UseAutofacMiddleware(container);

            var webApiDependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Adding to the pipeline with our own middleware
            app.Use(async (context, next) =>
            {
                // Add Header
                context.Response.Headers["Product"] = "Web Api Self Host";

                // Call next middleware
                await next.Invoke();
            });
            
            // Custom Middleare
            app.Use(typeof(CustomMiddleware));

            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            config.DependencyResolver = webApiDependencyResolver;

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ConfigureOAuth(app, container);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseAutofacWebApi(config);

            // Web Api
            app.UseWebApi(config);

            // File Server
            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                EnableDefaultFiles = true,
                DefaultFilesOptions = { DefaultFileNames = {"index.html"}},
                FileSystem = new PhysicalFileSystem("Assets"),
                StaticFileOptions = { ContentTypeProvider = new CustomContentTypeProvider() }
            };

            app.UseFileServer(options);

            // Nancy
            app.UseNancy();
        }

        private void ConfigureOAuth(IAppBuilder app, IContainer container)
        {
            var OAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = container.Resolve<IOAuthAuthorizationServerProvider>(),
                RefreshTokenProvider = container.Resolve<IAuthenticationTokenProvider>()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
