using System;
using System.IO;
using ChatApplication;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebSocketManager;

namespace FirServer
{
    public class Startup
    {
        public static ILoggerRepository repository { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRouting();
            services.AddWebSocketManager();
            services.AddCors(o => o.AddPolicy("WebPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.CookieName = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseCors("WebPolicy");
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            var handler = serviceProvider.GetService<AppServer>();
            if (handler != null)
            {
                handler.Initialize();
                app.MapWebSocketManager("/chat", handler);
            }
            ILog log = LogManager.GetLogger(repository.Name, typeof(Startup));
            log.Info("Startup.Configure------------------->>>>");
        }
    }
}
