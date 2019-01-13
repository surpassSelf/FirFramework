using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using FirServer;
using log4net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;

namespace WebServer
{
    public class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(Program));

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel(options =>
            {
                options.Listen(IPAddress.Any, 5000);  // http:localhost:5000
                options.Listen(IPAddress.Any, 80);         // http:*:80
                options.Listen(IPAddress.Any, 443, listenOptions =>
                {
                    var pfxFile = Path.Combine(Directory.GetCurrentDirectory(), "server.pfx");
                    logger.Info("pfx:" + pfxFile);
                    listenOptions.UseHttps(pfxFile, "510521fls");
                });
            })
            .UseStartup<Startup>()
            .Build();
    }
}
