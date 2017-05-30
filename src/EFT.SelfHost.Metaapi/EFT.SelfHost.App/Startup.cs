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

            app.Use(async (context, next) =>
            {
                context.Response.Headers["Product"] = "Web Api Self Host";
                await next.Invoke();
            });

            app.Use(typeof(CustomMiddleware));

            var config = new HttpConfiguration();

            config.DependencyResolver = webApiDependencyResolver;

            config.Routes.MapHttpRoute(
                    name: "ControllerAndAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, extension = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ConfigureOAuth(app, container);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseAutofacWebApi(config);

            app.UseWebApi(config);

            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                EnableDefaultFiles = true,
                DefaultFilesOptions = { DefaultFileNames = { "index.html" } },
                FileSystem = new PhysicalFileSystem("Assets"),
                StaticFileOptions = { ContentTypeProvider = new CustomContentTypeProvider() }
            };

            app.UseFileServer(options);

            app.UseNancy();


            InitializeData(container);
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

        private void InitializeData(IContainer container)
        {
            var mongoContext = container.Resolve<IMongoContext>();

            if (mongoContext.Clients.Count() == 0)
            {
                mongoContext.Clients.Insert(new Client
                {
                    Id = "ngAuthApp",
                    Secret = Helper.GetHash("aytac@eftsoftware.com"),
                    Name = "AngularJS front-end Application",
                    ApplicationType = Models.ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*",
                });

                mongoContext.Clients.Insert(new Client
                {
                    Id = "consoleApp",
                    Secret = Helper.GetHash("aytac@eftsoftware.com"),
                    Name = "Console Application",
                    ApplicationType = Models.ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                });
            }
        }
    }
}
